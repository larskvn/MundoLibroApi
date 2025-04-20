using BookNest.Application.Dtos.Book;

namespace BookNest.Application.Dtos.Editorial;

public class EditorialWithBooksDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<BookSmallDto> Books { get; set; }
}