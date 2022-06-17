using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class MeterType:IAggregateRoot
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public List<Provider>? Providers { get; set; }
    
}