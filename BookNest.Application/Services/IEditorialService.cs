namespace BookNest.Application.Services;

using Dtos.Editorial;

public interface IEditorialService
{
    Task<IReadOnlyList<EditorialSmallDto>> FindAllAsync();
    Task<EditorialDto> FindByIdAsync(int id);
    Task<EditorialWithBooksDto> FindBooksByEditorialIdAsync(int id);
    Task<EditorialSmallDto> CreateAsync(EditorialBodyDto editorialBody);
    Task<EditorialSmallDto> UpdateAsync(int id, EditorialBodyDto editorialBody);
    Task<EditorialStatusDto> DeleteAsync(int id);
}