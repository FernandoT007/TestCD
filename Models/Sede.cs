namespace SedesFunction.Models;

public class Sede
{
    public Guid  Id { get; set; }
    public required string Name { get; set; }
    public required string ImageUrl { get; set; }
}