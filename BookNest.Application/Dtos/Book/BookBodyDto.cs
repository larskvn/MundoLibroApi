namespace BookNest.Application.Dtos.Book;

public class BookBodyDto
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public string Edition { get; set; }
    public int Year { get; set; }
    public int EditorialId { get; set; }
}