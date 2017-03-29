
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Infrastructure.Modules;
using SF.Module.ActivityLog.Data;
using System;
using System.Collections.Generic;

namespace SF.Module.ActivityLog
{
    /// <summary>
    /// ActivityLog模块初始化
    /// </summary>
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddActivityService
                };
            }
        }
        public void AddActivityService(IServiceCollection services)
        {
            services.AddTransient<IActivityUnitOfWork, ActivityUnitOfWork>();
        }
    }
}
