using KomunalkaUA.Domain.Interfaces;

namespace KomunalkaUA.Domain.Models;

public class Address:IAggregateRoot
{
    public int Id { get; set; }
    public int? CityId { get; set; }
    public string? Street { get; set; }
    public string? Building { get; set; }
    public string? FlatNumber { get; set; }
    public City? City { get; set; }
    public List<Flat> Flats { get; set; }
}