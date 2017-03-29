using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SF.Core.Infrastructure.Modules;
using SF.Module.SimpleData.Data;
using System;
using System.Collections.Generic;

namespace SF.Module.SimpleData
{
    /// <summary>
    /// SimpleData模块初始化
    /// </summary>
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddLocalizationService
                };
            }
        }

        public void AddLocalizationService(IServiceCollection services)
        {
            services.AddDbContext<UnicornsContext>((serviceProvider, options) =>
               options.UseSqlServer("Server=.;Database=Unicorns_Base;uid=sa;pwd=wharton@168;Pooling=True;Min Pool Size=1;Max Pool Size=100;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;",
                    b => b.MigrationsAssembly("SF.WebHost"))
                      .UseInternalServiceProvider(serviceProvider));

            services.TryAddScoped<ISimpleDataUnitOfWork, SimpleDataUnitOfWork>();
        }
    }
}
