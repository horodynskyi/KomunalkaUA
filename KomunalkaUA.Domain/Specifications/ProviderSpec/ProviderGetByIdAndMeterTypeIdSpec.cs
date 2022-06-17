using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.ProviderSpec;

public class ProviderGetByCityIdAndMeterTypeIdSpec:Specification<Provider>
{
    public ProviderGetByCityIdAndMeterTypeIdSpec(int cityId,int meterTypeId)
    {
        Query
            .Where(x => x.CityId == cityId && x.MeterTypeId == meterTypeId);
    }
}