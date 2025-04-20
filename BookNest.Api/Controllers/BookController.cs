using BookNest.Application.Dtos.Book;
using BookNest.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/libros")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? page = null, [FromQuery] int? pageSize = null)
        {
            var books = await _bookService.FindAllAsync(page, pageSize);
            return Ok(books);
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var book = await _bookService.FindByIdAsync(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("editorial/{editorialId:int}")]
        public async Task<IActionResult> GetByEditorial(int editorialId)
        {
            try
            {
                var books = await _bookService.FindByEditorialIdAsync(editorialId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("autor/{author}")]
        public async Task<IActionResult> GetByAuthor(string author)
        {
            try
            {
                var books = await _bookService.FindByAuthorAsync(author);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("disponibles")]
        public async Task<IActionResult> GetAvailable()
        {
            var books = await _bookService.FindAvailableAsync();
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookBodyDto dto)
        {
            try
            {
                var created = await _bookService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = ex.Errors.Select(e => new
                    {
                        campo = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookBodyDto dto)
        {
            try
            {
                var updated = await _bookService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = ex.Errors.Select(e => new
                    {
                        campo = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _bookService.DeleteAsync(id);
                
                return Ok(new { status = "success", data = deleted });
            }
            catch (Exception e)
            {
                return NotFound(new { status = "error", message = e.Message });
            }
            
            
        }
        
       
    
        
    }
}
