namespace KomunalkaUA.Domain.Models;

public class Checkout
{
    public int Id { get; set; }
    public int? FlatId { get; set; }
    public int? TariffId { get; set; }
    public Flat? Flat { get; set; }
    public Tariff? Tariff { get; set; }
}