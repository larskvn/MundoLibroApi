using BookNest.Application.Dtos.Editorial;

namespace BookNest.Application.Dtos.Book;

public class BookDto
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public string Edition { get; set; }
    public int Year { get; set; }
    public int EditorialId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int Status { get; set; } 
    public EditorialDto Editorial { get; set; } 
}