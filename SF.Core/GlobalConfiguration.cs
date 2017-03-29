using System.Collections.Generic;

namespace SF.Core
{
    public static class GlobalConfiguration
    {
        static GlobalConfiguration()
        {
        }

        /// <summary>
        /// 获取或设置包含web可执行文件的目录的绝对路径
        /// </summary>
        public static string WebRootPath { get; set; }

        /// <summary>
        /// 获取或设置包含应用程序的目录的绝对路径
        /// </summary>
        public static string ContentRootPath { get; set; }

        /// <summary>
        /// 错误页字典
        /// </summary>
        public static IDictionary<int, string> ErrorPages { get; } = new Dictionary<int, string>();
    }
}
