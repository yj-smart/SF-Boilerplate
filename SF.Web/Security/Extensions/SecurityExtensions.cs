using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.StartupTask;
using SF.Web.Security.Attributes;
using SF.Web.Security.AuthorizationHandlers.Custom;
using SF.Web.Security.Filters;

namespace SF.Web.Security
{
    public static class SecurityExtensions
    {
        /// <summary>
        /// 插件基本服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddSecuritys(this IServiceCollection services)
        {
            //自定义Claim
            //services.TryAddScoped<ICustomClaimProvider, DoNothingCustomClaimProvider>();
            //services.TryAddScoped<IUserClaimsPrincipalFactory<UserEntity>, SiteUserClaimsPrincipalFactory<UserEntity,RoleEntity>>();

            services.AddScoped<IAuthorizationHandler, RolesPermissionsHandler>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddTransient<IFilterProvider, DependencyFilterProvider>();
            services.AddTransient<IFilterMetadata, AdminFilter>();
            services.AddTransient<IFilterMetadata, SFAuthorizationFilter>();

            services.AddTransient<IPermissionProvider, GobalPermissionProvider>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IStartupTask, RolePermissionStartupTask>();
            services.AddTransient<IAuthorizationHelper, AuthorizationHelper>();
        }
        public static void AddSecurityOption(this MvcOptions mvcOptions)
        {
           // mvcOptions.Filters.AddService(typeof(SFAuthorizationFilter));
        }

        public static void UseSecurity(this IApplicationBuilder applicationBuilder)
        {
            // Ensure the shell tenants are loaded when a request comes in
            // and replaces the current service provider for the tenant's one.

        }
    }
}
