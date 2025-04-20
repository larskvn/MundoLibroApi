namespace BookNest.Application.Dtos.Book;

public class BookEditorialDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public int EditorialId { get; set; }
    public string EditorialName { get; set; }
}