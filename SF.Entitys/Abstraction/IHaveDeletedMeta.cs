
namespace SF.Entitys.Abstraction
{
    using System;

#if !NET20

    /// <summary>
    /// 有关实体删除的元数据信息
    /// </summary>
    /// <typeparam name="TDeletedBy">The identifier or entity type</typeparam>
    public interface IHaveDeletedMeta<TDeletedBy>
    {
        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was soft deleted
        /// </summary>
        DateTimeOffset? DeletedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which soft deleted this entity
        /// </summary>
        TDeletedBy DeletedBy { get; set; }
    }

    /// <summary>
    /// 有关实体删除的元数据信息接口
    /// </summary>
    public interface IHaveDeletedMeta : IHaveDeletedMeta<string>
    {

    }

#endif
}
