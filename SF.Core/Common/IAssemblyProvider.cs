
using SF.Core.Infrastructure.Modules;
using System.Collections.Generic;
using System.Reflection;

namespace SF.Core.Common
{
    /// <summary>
    /// 提供程序集信息
    /// </summary>
    public interface IAssemblyProvider
    {
        /// <summary>
        /// 获取程序集信息
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies(string path);

        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        IEnumerable<ModuleInfo> GetModules(string path);
    }
}