
namespace SF.Entitys.Abstraction
{
    using System;

#if !NET20

    /// <summary>
    /// 有关实体创建的元数据信息接口
    /// </summary>
    /// <typeparam name="TCreatedBy">The identifier or entity type</typeparam>
    public interface IHaveCreatedMeta<TCreatedBy>
    {
        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was created
        /// </summary>
        DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which first created this entity
        /// </summary>
        TCreatedBy CreatedBy { get; set; }
    }

    /// <summary>
    /// 有关实体创建的元数据信息接口
    /// </summary>
    public interface IHaveCreatedMeta : IHaveCreatedMeta<string>
    {

    }

#endif

}
