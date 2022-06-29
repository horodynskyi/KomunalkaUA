using KomunalkaUA.Domain.Interfaces;

namespace KomunalkaUA.Domain.Models;

public class CheckoutPreMeterCheckout:IAggregateRoot
{
    public int Id { get; set; }
    public int? CheckoutId { get; set; }
    public int? PreMeterCheckoutId { get; set; }
    public Checkout Checkout { get; set; }
    public PreMeterCheckout? PreMeterCheckout { get; set; }
}