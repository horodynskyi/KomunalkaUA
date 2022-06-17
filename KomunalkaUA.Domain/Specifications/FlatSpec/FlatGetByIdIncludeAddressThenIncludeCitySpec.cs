using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatSpec;

public class FlatGetByIdIncludeAddressThenIncludeCitySpec:Specification<Flat>,ISingleResultSpecification<Flat>
{
    public FlatGetByIdIncludeAddressThenIncludeCitySpec(int id)
    {
        Query
            .Include(x => x.Address)
            .ThenInclude(x => x.City)
            .Where(x => x.Id == id);
    }
}