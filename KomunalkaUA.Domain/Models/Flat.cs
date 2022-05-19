using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class Flat : IAggregateRoot
{
    public int Id { get; set; }
    public string? CardNumber { get; set; }
    public int? OwnerId { get; set; }
    public int? TenantId { get; set; }
    public int? WatterMeterId { get; set; }
    public int? GasMeterId { get; set; }
    public int? ElectricMeterId { get; set; }

    public User? Owner { get; set; } 
    public User? Tenant { get; set; } 
    public WatterMeter? WatterMeter { get; set; }
    public GasMeter? GasMeter { get; set; }
    public ElectricMeter? ElectricMeter { get; set; }
    
    public List<Checkout>? Checkouts { get; set; }
}