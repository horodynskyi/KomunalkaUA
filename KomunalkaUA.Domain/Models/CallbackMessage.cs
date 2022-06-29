using KomunalkaUA.Domain.Interfaces;

namespace KomunalkaUA.Domain.Models;

public class CallbackMessage:IAggregateRoot
{
    public long Id { get; set; }
    public int? UserId { get; set; }
}