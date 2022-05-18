using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class Role:IAggregateRoot
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<User>? Users { get; set; }
}