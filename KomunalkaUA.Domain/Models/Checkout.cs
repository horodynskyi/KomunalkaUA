namespace KomunalkaUA.Domain.Models;

public class Checkout
{
    public int Id { get; set; }
    public int? FlatId { get; set; }
    public int? FlatMeterId { get; set; }
    public int? StartValue { get; set; }
    public int? EndValue { get; set; }
    public int? Rent { get; set; }
    public FlatMeter? FlatMeter { get; set; }
    
}