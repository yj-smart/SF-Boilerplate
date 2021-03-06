﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Infrastructure.Modules;
using SF.Module.Blog.Data;
using SF.Module.Blog.Data.Uow;
using System;
using System.Collections.Generic;

namespace SF.Module.Blog
{
    /// <summary>
    /// Blog模块初始化
    /// </summary>
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {

        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddCoreServices
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [10000] = this.UseMvc
                };
            }
        }

        /// <summary>
        /// 全局服务注册
        /// </summary>
        /// <param name="services"></param>
        private void AddCoreServices(IServiceCollection services)
        {
            services.AddDbContext<BlogContext>((serviceProvider, options) =>
             options.UseSqlServer("Server=.;Database=SF_Team_Blog;uid=sa;pwd=123456;Pooling=True;Min Pool Size=1;Max Pool Size=100;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;",
                  b => b.MigrationsAssembly("SF.Module.Blog"))
                    .UseInternalServiceProvider(serviceProvider));
            services.AddTransient<IBlogUnitOfWork, BlogUnitOfWork>();
        }

        /// <summary>
        /// 全局构建
        /// </summary>
        /// <param name="applicationBuilder"></param>
        private void UseMvc(IApplicationBuilder applicationBuilder)
        {

        }
    }
}
