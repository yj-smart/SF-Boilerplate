using SF.Core.Infrastructure.Modules;
using System.Collections.Generic;

namespace SF.Web.Modules
{
    public interface IModuleManager
    {
        List<ModuleInfo> GetModule();
        void InstallModule(ModuleInfo module);
        void UnInstallModule(ModuleInfo module);
    }
}
