using Ardalis.Specification;

namespace KomunalkaUA.Shared;

public interface IRepository<T>: IRepositoryBase<T> where T : class,IAggregateRoot
{
    
}