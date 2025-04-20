namespace BookNest.Application.Services.Implemetation;

using System.Linq.Expressions;
using AutoMapper;
using Dtos.Editorial;
using FluentValidation;
using Library.Domain.Models;
using Library.Domain.Repositories;

public class EditorialService: IEditorialService
{
    private readonly IEditorialRepository _editorialRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<EditorialBodyDto> _editorialBodyDtoValidator;

    public EditorialService(IEditorialRepository editorialRepository, IMapper mapper,  IValidator<EditorialBodyDto> editorialBodyDtoValidator)
    {
        _editorialRepository = editorialRepository;
        _mapper = mapper;
        _editorialBodyDtoValidator = editorialBodyDtoValidator;
        
    }
    
    public async Task<IReadOnlyList<EditorialSmallDto>> FindAllAsync()
    {
        var editorials = await _editorialRepository.FindAllAsync(e => e.Status == 1);
        return _mapper.Map<IReadOnlyList<EditorialSmallDto>>(editorials);
    }

    public async Task<EditorialDto> FindByIdAsync(int id)
    {
        var editorial = await _editorialRepository.FindByIdAsync(id);
        if (editorial is null)
            throw new Exception($"No se encontró la editorial con id {id}");

        return _mapper.Map<EditorialDto>(editorial);
    }

    public async Task<EditorialWithBooksDto> FindBooksByEditorialIdAsync(int id)
    {
        var includes = new List<Expression<Func<Editorial, object>>> { e => e.Books };
        var editorial = await _editorialRepository.FindFirstOrDefaultAsync(e => e.Id == id, includes);

        if (editorial is null)
            throw new Exception($"No se encontró la editorial con id {id}");

        return _mapper.Map<EditorialWithBooksDto>(editorial);
    }

    public async Task<EditorialSmallDto> CreateAsync(EditorialBodyDto editorialBody)
    {
        var validation = await _editorialBodyDtoValidator.ValidateAsync(editorialBody);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var editorial = _mapper.Map<Editorial>(editorialBody);
        editorial.RegistrationDate = DateTime.UtcNow;
        editorial.Status = 1;

        await _editorialRepository.SaveAsync(editorial);
        return _mapper.Map<EditorialSmallDto>(editorial);
    }

    public async Task<EditorialSmallDto> UpdateAsync(int id, EditorialBodyDto editorialBody)
    {
        var validation = await _editorialBodyDtoValidator.ValidateAsync(editorialBody);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var editorial = await _editorialRepository.FindByIdAsync(id);
        if (editorial == null)
            throw new Exception($"Editorial con id {id} no encontrada");

        _mapper.Map(editorialBody, editorial);

        await _editorialRepository.SaveAsync(editorial);

        return _mapper.Map<EditorialSmallDto>(editorial);
    }

    public async Task<EditorialStatusDto> DeleteAsync(int id)
    {
        var includes = new List<Expression<Func<Editorial, object>>> { e => e.Books };
    
        var editorial = await _editorialRepository.FindFirstOrDefaultAsync(e => e.Id == id, includes);

        if (editorial == null)
            throw new Exception($"Editorial con id {id} no encontrada.");

        if (editorial.Books != null && editorial.Books.Any())
            throw new Exception($"No se puede eliminar la editorial con id {id} porque tiene libros asociados.");

        editorial.Status = 0; 

        await _editorialRepository.SaveAsync(editorial);

        return _mapper.Map<EditorialStatusDto>(editorial);
    }
}