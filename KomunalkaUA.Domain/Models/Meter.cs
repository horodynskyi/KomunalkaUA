using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class Meter: IAggregateRoot
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public int? Value { get; }
    public MeterType  MeterType { get; set; }
    public List<Flat>? ElectricMeters { get; set; }
    public List<Flat>? GasMeters { get; set; }
    public List<Flat>? WaterMeter { get; set; }
}