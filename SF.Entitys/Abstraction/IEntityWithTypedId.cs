namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// 实体Id类型接口
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntityWithTypedId<TId> : IEntity
    {
        TId Id { get; set; }
    }
}
