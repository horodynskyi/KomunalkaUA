using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatSpec;

public class FlatGetByTenantIdIncludeTanandAddressSpec:Specification<Flat>,ISingleResultSpecification<Flat>
{
    public FlatGetByTenantIdIncludeTanandAddressSpec(long id)
    {
        Query
            .Include(x => x.Tenant)
            .Include(x => x.Address)
            .Where(x => x.TenantId == id);
    }
}
