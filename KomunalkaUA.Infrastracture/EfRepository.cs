using Ardalis.Specification.EntityFrameworkCore;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Infrastracture.Database;

namespace KomunalkaUA.Infrastracture;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(DataContext dbContext) : base(dbContext)
    {
    }
}