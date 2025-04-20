using BookNest.Application.Dtos.Book;

namespace BookNest.Application.Services;

public interface IBookService
{
    Task<IReadOnlyList<BookSmallDto>> FindAllAsync(int? page = null, int? pageSize = null);
    Task<BookDto> FindByIdAsync(int id);
    Task<IReadOnlyList<BookEditorialDto>> FindByEditorialIdAsync(int editorialId);
    Task<IReadOnlyList<BookSmallDto>> FindByAuthorAsync(string author);
    Task<IReadOnlyList<BookSmallDto>> FindAvailableAsync();
    Task<BookSmallDto> CreateAsync(BookBodyDto bookBody);
    Task<BookSmallDto> UpdateAsync(int id, BookBodyDto bookBody);
    Task<BookStatusDto> DeleteAsync(int id);

    
}