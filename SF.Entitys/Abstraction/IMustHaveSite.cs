namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// 为必须具有SiteId的实体实现此接口
    /// </summary>
    public interface IMustHaveSite
    {
        /// <summary>
        /// SiteId of this entity.
        /// </summary>
        long SiteId { get; set; }
    }
}