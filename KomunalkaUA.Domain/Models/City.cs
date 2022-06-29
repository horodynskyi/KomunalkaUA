using KomunalkaUA.Domain.Interfaces;

namespace KomunalkaUA.Domain.Models;

public class City:IAggregateRoot
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Region { get; set; }
    public List<Address>? Addresses { get; set; }
    public List<Provider>? Providers { get; set; }
}