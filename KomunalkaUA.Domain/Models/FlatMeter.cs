using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class FlatMeter:IAggregateRoot
{
    public int Id { get; set; }
    public int? FlatId { get; set; }
    public int? MetterId { get; set; }
    public Flat? Flat { get; set; }
    public Meter? Meter { get; set; }
    public List<Checkout>? Checkouts { get; set; }
}