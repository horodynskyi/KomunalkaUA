using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatSpec;

public class FlatGetByIdIncludeAddressSpec:Specification<Flat>,ISingleResultSpecification<Flat>
{
    public FlatGetByIdIncludeAddressSpec(int id)
    {
        Query
            .Where(x => x.Id == id)
            .Include(x => x.Address);
    }
}