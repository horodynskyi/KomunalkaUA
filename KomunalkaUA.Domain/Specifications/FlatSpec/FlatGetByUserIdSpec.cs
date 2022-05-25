using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatSpec;

public class FlatGetByOwnerIdIncludeAddressSpec:Specification<Flat>
{
    public FlatGetByOwnerIdIncludeAddressSpec(long id)
    {
        Query
            .Where(x => x.OwnerId == id)
            .Include(x => x.Address);
    }
}