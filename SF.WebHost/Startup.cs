using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Core;
using SF.Core.Abstraction.Mapping;
using SF.Core.Common;
using SF.Core.Infrastructure.Modules;
using SF.Core.StartupTask;
using StackExchange.Profiling;
using StackExchange.Profiling.Mvc;
using StackExchange.Profiling.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using simpleGlobal = SF.Core;

namespace SF.WebHost
{
    public class Startup
    {
        //定义一种检索服务对象的机制，服务对象是为其他对象提供自定义支持的对象
        protected IServiceProvider serviceProvider;
        //提供有关运行应用程序的Web托管环境的信息
        private readonly IHostingEnvironment _hostingEnvironment;
        //提供程序集信息
        protected IAssemblyProvider assemblyProvider;
        protected ILogger<Startup> logger;

        public Startup(IHostingEnvironment env, IServiceProvider serviceProvider)
         : this(env, serviceProvider, new AssemblyProvider(serviceProvider))
        {
            this.logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Startup>();
        }

        public Startup(IHostingEnvironment env, IServiceProvider serviceProvider, IAssemblyProvider assemblyProvider)
        {
            this.serviceProvider = serviceProvider;
            this.assemblyProvider = assemblyProvider;
            this._hostingEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            // you can use whatever file name you like and it is probably a good idea to use a custom file name
            // just an a small extra protection in case hackers try some kind of attack based on knowing the name of the file
            // it should not be possible for anyone to get files outside of wwwroot using http requests
            // but every little thing you can do for stronger security is a good idea
            builder.AddJsonFile("Config/simpleauthsettings.json", optional: true);
            builder.AddJsonFile("Config/ratelimitsettings.json", optional: true);
            builder.AddJsonFile("Config/cache.json", optional: false);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();


            foreach (var c in Configuration.GetSection("ErrorPages").GetChildren())
            {
                var key = Convert.ToInt32(c.Key);
                if (!GlobalConfiguration.ErrorPages.Keys.Contains(key))
                {
                    GlobalConfiguration.ErrorPages.Add(key, c.Value);
                }
            }
        }

        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            this.DiscoverAssemblies();

            simpleGlobal.GlobalConfiguration.WebRootPath = _hostingEnvironment.WebRootPath;
            simpleGlobal.GlobalConfiguration.ContentRootPath = _hostingEnvironment.ContentRootPath;

            // Add Application Insights data collection services to the services container.
            services.AddApplicationInsightsTelemetry(Configuration);
            //services.AddSingleton(_ => Configuration);
            //services.AddSingleton(_ => services);
            services.AddSingleton(typeof(IServiceCollection), (o) => { return services; });

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
            {
                extension.SetServiceProvider(this.serviceProvider);
                extension.SetConfigurationRoot(this.Configuration);
            }

            foreach (Action<IServiceCollection> prioritizedConfigureServicesAction in this.GetPrioritizedConfigureServicesActions())
            {
                this.logger.LogInformation("Executing prioritized ConfigureServices action '{0}' of {1}", this.GetActionMethodInfo(prioritizedConfigureServicesAction));
                prioritizedConfigureServicesAction(services);
            }


            //实例并通过构造函数初始化Mapper配置
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                foreach (IAutoMapperConfiguration mappingRegistration in ExtensionManager.GetInstances<IAutoMapperConfiguration>())
                {
                    mappingRegistration.MapperConfigurationToExpression(cfg);
                }
            });
            services.AddSingleton<IMapper>(sp => mapperConfiguration.CreateMapper());

            //使用静态映射实例初始化AutoMapper
            Mapper.Initialize(cfg =>
            {
                foreach (IAutoMapperConfiguration mappingRegistration in ExtensionManager.GetInstances<IAutoMapperConfiguration>())
                {
                    mappingRegistration.MapperConfigurationToExpression(cfg);
                }
            });


            var serviceProvider = services.BuildServiceProvider();

            var sfStarter = serviceProvider.GetService<ISFStarter>();
            sfStarter.Run();


            if (_hostingEnvironment.IsDevelopment())
            {
                services.AddMiniProfiler();
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IMemoryCache cache)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Development"))
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                //  app.UseDatabaseErrorPage();
                app.UseMiniProfiler(new MiniProfilerOptions
                {
                    RouteBasePath = "~/profiler",
                    SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter(),
                    Storage = new MemoryCacheStorage(cache, TimeSpan.FromMinutes(60))
                });
            }

            foreach (Action<IApplicationBuilder> prioritizedConfigureAction in this.GetPrioritizedConfigureActions())
            {
                this.logger.LogInformation("Executing prioritized Configure action '{0}' of {1}", this.GetActionMethodInfo(prioritizedConfigureAction));
                prioritizedConfigureAction(app);
            }
        }

        /// <summary>
        /// 加载程序集、模块的相关信息
        /// </summary>
        private void DiscoverAssemblies()
        {
            string extensionsPath = this.Configuration["Modules:Path"];
            IEnumerable<Assembly> assemblies = this.assemblyProvider.GetAssemblies(
              string.IsNullOrEmpty(extensionsPath) ?
                null : this.serviceProvider.GetService<IHostingEnvironment>().ContentRootPath
            );
            ExtensionManager.SetAssemblies(assemblies.ToList());

            IEnumerable<ModuleInfo> modules = this.assemblyProvider.GetModules(
              string.IsNullOrEmpty(extensionsPath) ?
                null : this.serviceProvider.GetService<IHostingEnvironment>().ContentRootPath + extensionsPath);
            ExtensionManager.SetModules(modules.ToList());
        }

        /// <summary>
        /// 获取模块服务注册方法集合
        /// </summary>
        /// <returns></returns>
        private Action<IServiceCollection>[] GetPrioritizedConfigureServicesActions()
        {
            List<KeyValuePair<int, Action<IServiceCollection>>> configureServicesActionsByPriorities = new List<KeyValuePair<int, Action<IServiceCollection>>>();

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
                if (extension.ConfigureServicesActionsByPriorities != null)
                    configureServicesActionsByPriorities.AddRange(extension.ConfigureServicesActionsByPriorities);

            return this.GetPrioritizedActions(configureServicesActionsByPriorities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Action<IApplicationBuilder>[] GetPrioritizedConfigureActions()
        {
            List<KeyValuePair<int, Action<IApplicationBuilder>>> configureActionsByPriorities = new List<KeyValuePair<int, Action<IApplicationBuilder>>>();

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
                if (extension.ConfigureActionsByPriorities != null)
                    configureActionsByPriorities.AddRange(extension.ConfigureActionsByPriorities);

            return this.GetPrioritizedActions(configureActionsByPriorities);
        }

        private Action<T>[] GetPrioritizedActions<T>(IEnumerable<KeyValuePair<int, Action<T>>> actionsByPriorities)
        {
            return actionsByPriorities
              .OrderBy(actionByPriority => actionByPriority.Key)
              .Select(actionByPriority => actionByPriority.Value)
              .ToArray();
        }

        private string[] GetActionMethodInfo<T>(Action<T> action)
        {
            MethodInfo methodInfo = action.GetMethodInfo();

            return new string[] { methodInfo.Name, methodInfo.DeclaringType.FullName };
        }
    }
}