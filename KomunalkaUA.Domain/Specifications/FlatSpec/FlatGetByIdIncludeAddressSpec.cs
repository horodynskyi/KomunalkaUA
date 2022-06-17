using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.FlatSpec;

public class FlatGetByIdIncludeAddressSpec:Specification<Flat>,ISingleResultSpecification<Flat>
{
    public FlatGetByIdIncludeAddressSpec(int id)
    {
        Query
            .Include(x => x.Address)
            .Where(x => x.Id == id);
    }
}