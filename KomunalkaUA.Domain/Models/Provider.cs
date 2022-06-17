using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class Provider:IAggregateRoot
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public double? Rate { get; set; }
    public int? MeterTypeId { get; set; }
    public MeterType? Type { get; set; }
    public int? CityId { get; set; }
    public City? City { get; set; }
    public List<Meter>? Meters { get; set; }
}