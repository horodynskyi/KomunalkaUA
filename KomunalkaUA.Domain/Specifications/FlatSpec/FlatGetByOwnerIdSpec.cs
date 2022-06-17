using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatSpec;

public class FlatGetByOwnerIncludeAddressIdSpec:Specification<Flat>
{
    public FlatGetByOwnerIncludeAddressIdSpec(long id)
    {
        Query
            .Include(x => x.Address)
            .Where(x => x.Owner.Id == id);
    }
}