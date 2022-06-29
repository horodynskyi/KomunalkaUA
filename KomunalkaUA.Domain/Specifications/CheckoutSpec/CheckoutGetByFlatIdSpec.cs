using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.CheckoutSpec;

public class CheckoutGetByFlatIdSpec:Specification<Checkout>
{
    public CheckoutGetByFlatIdSpec(int flatId)
    {
        Query
            .Where(x => x.FlatId == flatId);
    }
}