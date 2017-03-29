using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Modules.Builder
{
    /// <summary>
    /// 模块配置构建
    /// </summary>
    public interface IModuleConfigBuilder
    {
        /// <summary>
        /// 构建配置
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        Task<ModuleConfig> BuildConfig(string filePath);
    }
}
