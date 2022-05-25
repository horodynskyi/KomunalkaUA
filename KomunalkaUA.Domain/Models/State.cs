using System.Data;
using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Shared;

namespace KomunalkaUA.Domain.Models;

public class State:IAggregateRoot
{
    public Guid Id { get; set; }
    public long? UserId { get; set; }
 
    public string? Value { get; set; }
    public StateType StateType { get; set; }
    public User? User { get; set; }
}