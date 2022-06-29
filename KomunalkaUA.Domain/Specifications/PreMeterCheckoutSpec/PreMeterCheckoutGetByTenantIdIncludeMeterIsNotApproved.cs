using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.PreMeterCheckoutSpec;

public class PreMeterCheckoutGetByTenantIdIncludeMeterIsNotApproved:Specification<PreMeterCheckout>
{
    public PreMeterCheckoutGetByTenantIdIncludeMeterIsNotApproved(long tenantId)
    {
        Query
            .Where(x => x.TenantId == tenantId)
            .Where(x => x.IsApproved == false);
            
        Query.AsSplitQuery();
        Query
            .Include(x => x.Meter)
            .Include(x => x.Meter.Provider);
    }
}