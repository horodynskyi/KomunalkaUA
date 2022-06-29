using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using Newtonsoft.Json;

namespace KomunalkaUA.Domain.Models;

public class Meter: IAggregateRoot
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public int? Value { get; set; } //TODO
    public int? ProviderId { get; set; }
    public Provider? Provider { get; set; }
    [JsonIgnore]
    public List<FlatMeter>? FlatMeters { get; set; }
    [JsonIgnore]
    public List<PreMeterCheckout>? PreMeterCheckouts { get; set; }
  
}