using Microsoft.AspNetCore.Http;
using SF.Module.LoggingStorage.Data;
using SF.Module.LoggingStorage.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Module.LoggingStorage.Services
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public class LogManager
    {
        public LogManager(
            ILogRepository logRepository,
            IHttpContextAccessor contextAccessor)
        {
            logRepo = logRepository;
            _context = contextAccessor?.HttpContext;
        }

        private readonly HttpContext _context;
        private CancellationToken CancellationToken => _context?.RequestAborted ?? CancellationToken.None;
        private ILogRepository logRepo;

        public int LogPageSize { get; set; } = 10;

        /// <summary>
        /// 按降序获取指定页
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="logLevel">日志级别</param>
        /// <returns></returns>
        public async Task<PagedQueryResult> GetLogsDescending(int pageNumber, int pageSize, string logLevel = "")
        {
            return await logRepo.GetPageDescending(pageNumber, pageSize, logLevel, CancellationToken);
        }

        /// <summary>
        /// 按升序获取指定页
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="logLevel">日志级别</param>
        /// <returns></returns>
        public async Task<PagedQueryResult> GetLogsAscending(int pageNumber, int pageSize, string logLevel = "")
        {
            return await logRepo.GetPageAscending(pageNumber, pageSize, logLevel, CancellationToken);
        }

        /// <summary>
        /// 删除指定日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteLogItem(Guid id)
        {
            await logRepo.Delete(id, CancellationToken.None);
        }

        /// <summary>
        /// 按级别删除日志
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <returns></returns>
        public async Task DeleteAllLogItems(string logLevel = "")
        {
            await logRepo.DeleteAll(logLevel, CancellationToken.None);
        }

        /// <summary>
        /// 按日期和级别删除日志
        /// </summary>
        /// <param name="cutoffUtc">日志日期</param>
        /// <param name="logLevel">日志级别</param>
        /// <returns></returns>
        public async Task DeleteOlderThan(DateTime cutoffUtc, string logLevel = "")
        {
            await logRepo.DeleteOlderThan(cutoffUtc, logLevel, CancellationToken.None);
        }
    }
}
