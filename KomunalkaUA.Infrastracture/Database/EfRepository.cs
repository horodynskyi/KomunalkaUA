using Ardalis.Specification.EntityFrameworkCore;
using KomunalkaUA.Shared;

namespace KomunalkaUA.Infrastracture.Database;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(DataContext dbContext) : base(dbContext)
    {
    }
}