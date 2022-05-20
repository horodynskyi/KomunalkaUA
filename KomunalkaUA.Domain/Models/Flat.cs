using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class Flat : IAggregateRoot
{
    public int Id { get; set; }
    public string? CardNumber { get; set; }
    public long? OwnerId { get; set; }
    public long? TenantId { get; set; }
    public int? WatterMeterId { get; set; }
    public int? GasMeterId { get; set; }
    public int? ElectricMeterId { get; set; }
    public int? AddressId { get; set; }
    

    public User? Owner { get; set; } 
    public User? Tenant { get; set; } 
    public Meter? WatterMeter { get; set; }
    public Meter? GasMeter { get; set; }
    public Meter? ElectricMeter { get; set; }
    public Address? Address { get; set; }
    public List<Checkout>? Checkouts { get; set; }
}