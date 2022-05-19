using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class User:IAggregateRoot
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Username { get; set; }
    public string? PhoneNumber { get; set; }
    public int? RoleId { get; set; }
    public Role? Role { get; set; }
    public List<Flat> Owners { get; set; }
    public List<Flat> Tenants { get; set; }
    public List<State> States { get; set; }
}