namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// 实体Id类型
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class EntityWithTypedId<TId> : ValidatableObject, IEntityWithTypedId<TId>
    {
        public TId Id { get;  set; }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        protected EntityWithTypedId()
        {

        }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="id">The id for the entity</param>
        protected EntityWithTypedId(TId id)
        {
            Id = id;
        }
    }
}
