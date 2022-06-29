using KomunalkaUA.Domain.Interfaces;

namespace KomunalkaUA.Domain.Models;

public class PreMeterCheckout:IAggregateRoot
{
    public int Id { get; set; }
    public int? FlatId { get; set; }
    public long? TenantId { get; set; }
    public int? StartValue { get; set; }
    public int? EndValue { get; set; }
    public int? MeterId { get; set; }
    public bool IsApproved { get; set; } = false;
    
    public Flat? Flat { get; set; }
    public Meter? Meter { get; set; }
    public User? Tenant { get; set; }
    public List<CheckoutPreMeterCheckout>? CheckoutPreMeterCheckouts { get; set; }
}