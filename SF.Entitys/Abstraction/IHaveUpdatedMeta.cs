
namespace SF.Entitys.Abstraction
{
    using System;

#if !NET20

    /// <summary>
    /// 有关实体更新的元数据信息
    /// </summary>
    /// <typeparam name="TUpdatedBy">The identifier or entity type</typeparam>
    public interface IHaveUpdatedMeta<TUpdatedBy>
    {
        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was last updated
        /// </summary>
        DateTimeOffset UpdatedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which last updated this entity
        /// </summary>
        TUpdatedBy UpdatedBy { get; set; }
    }

    /// <summary>
    /// 有关实体更新的元数据信息接口
    /// </summary>
    public interface IHaveUpdatedMeta : IHaveUpdatedMeta<string>
    {

    }

#endif
}
