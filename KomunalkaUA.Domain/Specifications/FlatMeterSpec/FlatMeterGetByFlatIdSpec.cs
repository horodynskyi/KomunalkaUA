using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatMeterSpec;

public class FlatMeterGetByFlatIdIncludeMeterProviderSpec:Specification<FlatMeter>
{
    public FlatMeterGetByFlatIdIncludeMeterProviderSpec(int flatId)
    {
        Query
            .Where(x => x.FlatId == flatId)
            .Include(x => x.Meter)
            .ThenInclude(x => x.Provider)
            .ThenInclude(x => x.Type);
    }
}