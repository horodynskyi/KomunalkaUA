using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;

namespace KomunalkaUA.Domain.Models;

public class Role:IAggregateRoot
{
    public int Id { get; set; }
    public RoleType? RoleType { get; set; }
    public List<User>? Users { get; set; }
}