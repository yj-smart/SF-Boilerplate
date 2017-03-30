
using SF.Module.LoggingStorage.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Module.LoggingStorage.Data
{
    /// <summary>
    /// 日志仓储接口
    /// </summary>
    public interface ILogRepository
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="logItem"></param>
        void AddLogItem(LogItem logItem);

        //Task<int> GetCount(string logLevel = "", CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 按升序获取指定页
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="logLevel">日志级别</param>
        /// <param name="cancellationToken">传播有关应取消操作的通知</param>
        /// <returns></returns>
        Task<PagedQueryResult> GetPageAscending(
            int pageNumber,
            int pageSize,
            string logLevel = "",
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 按降序获取指定页
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="logLevel">日志级别</param>
        /// <param name="cancellationToken">传播有关应取消操作的通知</param>
        /// <returns></returns>
        Task<PagedQueryResult> GetPageDescending(
            int pageNumber,
            int pageSize,
            string logLevel = "",
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 按级别删除日志
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="cancellationToken">传播有关应取消操作的通知</param>
        /// <returns></returns>
        Task DeleteAll(string logLevel = "", CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 删除指定日志
        /// </summary>
        /// <param name="logItemId">日志项Id</param>
        /// <param name="cancellationToken">传播有关应取消操作的通知</param>
        /// <returns></returns>
        Task Delete(Guid logItemId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 按日期和级别删除日志
        /// </summary>
        /// <param name="cutoffDateUtc">日志日期</param>
        /// <param name="logLevel">日志级别</param>
        /// <param name="cancellationToken">传播有关应取消操作的通知</param>
        /// <returns></returns>
        Task DeleteOlderThan(DateTime cutoffDateUtc, string logLevel = "", CancellationToken cancellationToken = default(CancellationToken));
    }
}
