using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Infrastructure.Modules;
using System;
using System.Collections.Generic;

namespace SF.Module.Demo
{
    /// <summary>
    /// Demo模块初始化
    /// </summary>
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {

        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddCoreServices,
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [0] = this.UseMvc
                };
            }
        }

        /// <summary>
        /// 全局服务注册
        /// </summary>
        /// <param name="services"></param>
        private void AddCoreServices(IServiceCollection services)
        {

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
