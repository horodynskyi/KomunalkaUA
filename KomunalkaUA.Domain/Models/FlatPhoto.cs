namespace KomunalkaUA.Domain.Models;

public class FlatPhoto
{
    public Guid Id { get; set; }
    public string? PhotoId { get; set; }
    public int? FlatId { get; set; }
    public Flat? Flat { get; set; }
    public Photo? Photo { get; set; }
}