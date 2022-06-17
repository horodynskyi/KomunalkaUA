using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatMeterSpec;

public class FlatMeterGetByIdIncludeMeterProviderSpec:Specification<FlatMeter>,ISingleResultSpecification<FlatMeter>
{
    public FlatMeterGetByIdIncludeMeterProviderSpec(int meterId)
    {
        Query
            .Where(x => x.MetterId == meterId)
            .Include(x => x.Meter)
            .ThenInclude(x => x.Provider)
            .ThenInclude(x => x.Type);
    }
}