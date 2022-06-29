using KomunalkaUA.Domain.Interfaces;

namespace KomunalkaUA.Domain.Models;

public class Flat : IAggregateRoot
{
    public int Id { get; set; }
    public string? CardNumber { get; set; }
    public long? OwnerId { get; set; }
    public long? TenantId { get; set; }
    public int? AddressId { get; set; }
    public int? Rent { get; set; }
    

    public User? Owner { get; set; } 
    public User? Tenant { get; set; }
    public Address? Address { get; set; }
    public List<FlatMeter>? FlatMeters { get; set; }
    public List<FlatPhoto>? Photos { get; set; }
    public List<PreMeterCheckout>? PreMeterCheckouts { get; set; }
    public List<Checkout>? Checkouts { get; set; }
}