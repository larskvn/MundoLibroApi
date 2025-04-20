
using AutoMapper;
namespace BookNest.Application.Dtos.Book.Profiles;

using Book = Library.Domain.Models.Book;

public class BookProfile:Profile
{

    public BookProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<Book, BookSmallDto>();
        CreateMap<Book, BookEditorialDto>()
            .ForMember(dest => dest.EditorialName, opt => opt.MapFrom(src => src.Editorial.Name));
        CreateMap<Book, BookBodyDto>().ReverseMap();
        CreateMap<Book, BookStatusDto>();
    }
}