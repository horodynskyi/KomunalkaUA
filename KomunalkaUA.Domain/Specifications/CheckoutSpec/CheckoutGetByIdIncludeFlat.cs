using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.CheckoutSpec;

public class CheckoutGetByIdIncludeFlatAddress:Specification<Checkout>,ISingleResultSpecification<Checkout>
{
    public CheckoutGetByIdIncludeFlatAddress(int id)
    {
        Query
            .Where(x => x.Id == id)
            .Include(x => x.Flat)
            .ThenInclude(x => x.Address);
    }
}