using SF.Entitys.Abstraction;

namespace SF.Core.Abstraction.Domain
{
    public interface IRules<T> where T : IEntity
    {
        bool AllowDelete(T entity);
    }
}
