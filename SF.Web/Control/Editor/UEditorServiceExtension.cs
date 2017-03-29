﻿using Microsoft.Extensions.DependencyInjection;

namespace SF.Web.Control.Editor
{
    public static class UEditorServiceExtension
    {
        public static UEditorActionCollection AddUEditorService(
            this IServiceCollection services,
            string configFile = "config.json",
            bool isCache = false)
        {
            Config.ConfigFile = configFile;
            Config.noCache = !isCache;

            var actions = new UEditorActionCollection();
            services.AddSingleton(actions);
            services.AddSingleton<UEditorService>();

            return actions;
        }
    }
}
