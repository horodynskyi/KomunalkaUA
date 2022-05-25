namespace KomunalkaUA.Domain.Models;

public class Photo
{
    public string Id { get; set; }
    public bool IsDeleted { get; set; }
    public List<FlatPhoto>? FlatPhotos { get; set; }
}