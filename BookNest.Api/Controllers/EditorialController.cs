using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

using BookNest.Application.Dtos.Editorial;
using BookNest.Application.Services;

[ApiController]
[Route("api/editoriales")]
public class EditorialController : ControllerBase
{
    private readonly IEditorialService _editorialService;

    public EditorialController(IEditorialService editorialService)
    {
        _editorialService = editorialService;
    }

    // GET /api/editoriales
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EditorialSmallDto>>> GetAllAsync(int id)
    {
        var editorials = await _editorialService.FindAllAsync();
        return Ok(editorials);
    }

    // GET /api/editoriales/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EditorialDto>> GetByIdAsync(int id)
    {
        var result = await _editorialService.FindByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    // GET /api/editoriales/{id}/libros
    [HttpGet("{id:int}/libros")]
    public async Task<ActionResult<EditorialWithBooksDto>> GetBooksByEditorialIdAsync(int id)
    {
        try
        {
            var result = await _editorialService.FindBooksByEditorialIdAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    
    [HttpPost]
    public async Task<ActionResult<EditorialSmallDto>> CreateAsync([FromBody] EditorialBodyDto dto)
    {
        var result = await _editorialService.CreateAsync(dto);
        return Created($"/api/editoriales/{result.Id}", result);
    }

   
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EditorialSmallDto>> UpdateAsync(int id, [FromBody] EditorialBodyDto dto)
    {
        var result = await _editorialService.UpdateAsync(id, dto);
        return Ok(result);
    }

    // DELETE /api/editoriales/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<EditorialStatusDto>> DeleteAsync(int id)
    {
        try
        {
            // Llamamos al servicio para intentar eliminar la editorial
            var result = await _editorialService.DeleteAsync(id);
            return Ok(result); // Retornamos la respuesta con el estado actualizado de la editorial
        }
        catch (Exception ex)
        {
            // Si ocurre un error, retornamos un mensaje con la excepción
            return Conflict(new
            {
                error = true,
                message = ex.Message
            });
        }
    }
}