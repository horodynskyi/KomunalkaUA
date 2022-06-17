using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.MeterSpec;

public class MeterGetFlatByMeterId:Specification<Meter>,ISingleResultSpecification<Meter>
{
    public MeterGetFlatByMeterId(int meterId)
    {
        Query
            .Include(x => x.FlatMeters);
    }
}