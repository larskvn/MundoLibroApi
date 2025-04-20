using System.Linq.Expressions;
using AutoMapper;
using BookNest.Application.Dtos.Book;
using Library.Domain.Models;
using Library.Domain.Repositories;

namespace BookNest.Application.Services.Implemetation;

using FluentValidation;

public class BookService: IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<BookBodyDto>  _bookBodyDtoValidator;

    public BookService(IBookRepository bookRepository, IMapper mapper, IValidator<BookBodyDto> validator)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _bookBodyDtoValidator = validator;
    }
    
    public async Task<IReadOnlyList<BookSmallDto>> FindAllAsync(int? page = null, int? pageSize = null)
    {
        var books = await _bookRepository.FindAllAsync(b => b.Status == 1);
        var result = books.AsQueryable();

        if (page.HasValue && pageSize.HasValue)
        {
            result = result
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        return _mapper.Map<IReadOnlyList<BookSmallDto>>(result.ToList());
    }

    public async Task<BookDto> FindByIdAsync(int id)
    {
        var includes = new List<Expression<Func<Book, object>>>
        {
            b => b.Editorial
        };

        var book = await _bookRepository.FindFirstOrDefaultAsync(b => b.Id == id, includes);

        if (book is null)
            throw new Exception($"No se encontró el libro con id {id}");

        return _mapper.Map<BookDto>(book);
    }

    public async Task<IReadOnlyList<BookEditorialDto>> FindByEditorialIdAsync(int editorialId)
    {
        var books = await _bookRepository.FindAllAsync(
            b => b.EditorialId == editorialId && b.Status == 1,
            new List<Expression<Func<Book, object>>> { b => b.Editorial }
        );

        if (books == null || books.Count == 0)
            throw new Exception($"No se encontraron libros para la editorial con id {editorialId}");

        return _mapper.Map<IReadOnlyList<BookEditorialDto>>(books);
    }

    public async Task<IReadOnlyList<BookSmallDto>> FindByAuthorAsync(string author)
    {
        var books = await _bookRepository.FindAllAsync(
            b => b.Authors.Contains(author) && b.Status == 1
        );

        if (books == null || books.Count == 0)
            throw new Exception($"No se encontraron libros para el autor {author}");

        return _mapper.Map<IReadOnlyList<BookSmallDto>>(books);
    }

    public async Task<IReadOnlyList<BookSmallDto>> FindAvailableAsync()
    {
        var books = await _bookRepository.FindAllAsync(b => b.Status == 1);
        return _mapper.Map<IReadOnlyList<BookSmallDto>>(books);
    }

    public async Task<BookSmallDto> CreateAsync(BookBodyDto bookBody)
    {
        var validation = await _bookBodyDtoValidator.ValidateAsync(bookBody);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var book = _mapper.Map<Book>(bookBody);
        book.RegistrationDate = DateTime.UtcNow;
        book.Status = 1;

        await _bookRepository.SaveAsync(book);

        return _mapper.Map<BookSmallDto>(book);
    }

    public async Task<BookSmallDto> UpdateAsync(int id, BookBodyDto bookBody)
    {
        var validation = await _bookBodyDtoValidator.ValidateAsync(bookBody);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var book = await _bookRepository.FindByIdAsync(id);
        if (book is null)
            throw new Exception($"No se encontró el libro con id {id}");

        _mapper.Map(bookBody, book);
        await _bookRepository.SaveAsync(book);

        return _mapper.Map<BookSmallDto>(book);
    }

    public async Task<BookStatusDto> DeleteAsync(int id)
    {
        
        var book = await _bookRepository.FindByIdAsync(id);
        if (book == null)
        {
            throw new Exception($"No se encontró el libro con id {id}");
        }
        book.Status = 0;
        
        await _bookRepository.SaveAsync(book);

        return _mapper.Map<BookStatusDto>(book);
    }
}