using Ardalis.Specification;
using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatMeterSpec;

public class FlatMeterGetByTenantIdIncludeMeterFlatSpec:Specification<FlatMeter>,ISingleResultSpecification<FlatMeter>
{
    public FlatMeterGetByTenantIdIncludeMeterFlatSpec(long tenantId, int meterType)
    {
        Query
            .Include(x => x.Flat)
            .Where(x => x.Flat.TenantId == tenantId);
        Query.AsSplitQuery();
        Query
            .Include(x => x.Meter)
            .Include(x => x.Meter.Provider)
            .Where(x => x.Meter.Provider.MeterTypeId ==  meterType);
    }
}