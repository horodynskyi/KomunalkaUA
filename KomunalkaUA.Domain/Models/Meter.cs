namespace KomunalkaUA.Domain.Models;

public abstract class Meter<T>{
    public int Id { get; set; }
    public string? Number { get; set; }
    public int? Value { get; }
    public List<T>? Flats { get; set; }
}