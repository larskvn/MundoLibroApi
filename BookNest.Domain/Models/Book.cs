namespace Library.Domain.Models;

public class Book
{
    public int Id { get; set; }
    public string? Isbn { get; set; }
    public string? Title { get; set; }
    public string? Authors { get; set; }
    public string? Edition { get; set; }
    public int Year { get; set; }
    public int EditorialId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int Status { get; set; } = 1;
    
    public virtual Editorial? Editorial { get; set; }
    
}