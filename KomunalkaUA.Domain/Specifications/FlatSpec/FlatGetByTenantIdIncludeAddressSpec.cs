using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatSpec;

public class FlatGetByTenantIdIncludeAddressSpec:Specification<Flat>,ISingleResultSpecification<Flat>
{
    public FlatGetByTenantIdIncludeAddressSpec(long id)
    {
        Query
            .Include(x => x.Owner)
            .Include(x => x.Address)
            .Where(x => x.TenantId == id);
    }
}
