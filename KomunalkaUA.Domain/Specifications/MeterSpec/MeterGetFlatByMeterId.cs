using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.MeterSpec;

public class MeterGetFlatByMeterId:Specification<FlatMeter>,ISingleResultSpecification<FlatMeter>
{
    public MeterGetFlatByMeterId(int meterId)
    {
        Query
            .Where(x => x.MetterId==meterId)
            .Include(x => x.Flat);
    }
}