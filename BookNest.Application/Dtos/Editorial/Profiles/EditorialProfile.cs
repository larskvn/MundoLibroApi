using AutoMapper;

namespace BookNest.Application.Dtos.Editorial.Profiles;

using Library.Domain.Models;

public class EditorialProfile:Profile

{
    public EditorialProfile()
    {
        CreateMap<Editorial, EditorialDto>();
        CreateMap<Editorial, EditorialSmallDto>();
        CreateMap<Editorial, EditorialWithBooksDto>();
        CreateMap<Editorial, EditorialBodyDto>().ReverseMap();
        CreateMap<Editorial, EditorialStatusDto>();
    }
}