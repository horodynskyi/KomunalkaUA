using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.CheckoutSpec;

public class CheckoutIncludeFlatGetByTenantId:Specification<Checkout>
{
    public CheckoutIncludeFlatGetByTenantId(long tenantId)
    {
        Query
            .Include(x => x.Flat)
            .Where(x => x.Flat.TenantId == tenantId);
    }
}