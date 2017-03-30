using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using SF.Core.Infrastructure.Modules;
using SF.Module.LoggingStorage.Data;
using SF.Module.LoggingStorage.Services;
using System;
using System.Collections.Generic;

namespace SF.Module.LoggingStorage
{
    /// <summary>
    /// LoggingStorage模块初始化
    /// </summary>
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {

        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [0] = this.AddEFLoggingService
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [0] = this.UserEFLogging
                };
            }
        }

        /// <summary>
        /// 添加EF日志服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        private void AddEFLoggingService(IServiceCollection serviceCollection)
        {
            //添加范围
            serviceCollection.TryAddScoped<ILogRepository, LogRepository>();
            serviceCollection.TryAddScoped<LogManager>();

            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.LiterateConsole()
               .WriteTo.RollingFile("log-{Date}.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
              .CreateLogger();
        }

        private void UserEFLogging(IApplicationBuilder applicationBuilder)
        {
            var loggerFactory = applicationBuilder.ApplicationServices.GetService<ILoggerFactory>();

            var logRepo = applicationBuilder.ApplicationServices.GetService<ILogRepository>();
            // a customizable filter for logging
            LogLevel minimumLevel = LogLevel.Information;

            // add exclusions to remove noise in the logs
            var excludedLoggers = new List<string>
            {
                "Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware",
                "Microsoft.AspNetCore.Hosting.Internal.WebHost",
            };

            Func<string, LogLevel, bool> logFilter = (string loggerName, LogLevel logLevel) =>
            {
                if (logLevel < minimumLevel)
                {
                    return false;
                }

                if (excludedLoggers.Contains(loggerName))
                {
                    return false;
                }

                return true;
            };
            //寻找更好的日志记录方案
            //   loggerFactory.AddDbLogger(applicationBuilder.ApplicationServices, logFilter, logRepo);
            //Serilog
            ILoggerFactory loggerfactory = applicationBuilder.ApplicationServices.GetService<ILoggerFactory>();
            IApplicationLifetime appLifetime = applicationBuilder.ApplicationServices.GetService<IApplicationLifetime>();
            loggerfactory.AddSerilog();
            // Ensure any buffered events are sent at shutdown
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}
