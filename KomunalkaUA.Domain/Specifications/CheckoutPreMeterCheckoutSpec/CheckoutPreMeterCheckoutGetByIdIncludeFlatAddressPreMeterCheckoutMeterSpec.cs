using Ardalis.Specification;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Specifications.CheckoutPreMeterCheckoutSpec;

public class CheckoutPreMeterCheckoutGetByIdIncludeFlatAddressPreMeterCheckoutMeterSpec:Specification<CheckoutPreMeterCheckout>
{
    public CheckoutPreMeterCheckoutGetByIdIncludeFlatAddressPreMeterCheckoutMeterSpec(int checkoutId)
    {
        Query
            .Where(x => x.CheckoutId == checkoutId);
        Query.AsSplitQuery();
        Query
            .Include(x => x.Checkout);
        Query
            .Include(x => x.PreMeterCheckout)
            .ThenInclude(x => x.Flat)
            .ThenInclude(x => x.Address);

        Query
            .Include(x => x.PreMeterCheckout)
            .ThenInclude(x => x.Meter)
            .ThenInclude(x => x.Provider);
    }
}