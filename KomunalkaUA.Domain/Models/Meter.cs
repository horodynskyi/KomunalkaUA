using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class Meter: IAggregateRoot
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public int? Value { get; set; }
    public MeterType  MeterType { get; set; }
    public List<FlatMeter>? FlatMeters { get; set; }
}