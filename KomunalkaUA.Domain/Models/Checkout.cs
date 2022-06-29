using KomunalkaUA.Domain.Interfaces;
using NodaTime;

namespace KomunalkaUA.Domain.Models;

public class Checkout:IAggregateRoot
{
    public int Id { get; set; }
    public int? FlatId { get; set; }
    public int? PaymentSum { get; set; }
    public Instant? Date { get; set; }
    public Flat? Flat { get; set; }
    public List<CheckoutPreMeterCheckout>? CheckoutPreMeterCheckouts { get; set; }

}