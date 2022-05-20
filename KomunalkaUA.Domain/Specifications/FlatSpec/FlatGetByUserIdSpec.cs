using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatSpec;

public class FlatGetByOwnerIdSpec:Specification<Flat>,ISingleResultSpecification<Flat>
{
    public FlatGetByOwnerIdSpec(long id)
    {
        Query
            .Where(x => x.OwnerId == id);
    }
}