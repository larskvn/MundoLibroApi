using BookNest.Application.Dtos.Loan;
using BookNest.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;
[ApiController]
[Route("api/prestamos")]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    // GET: /api/prestamos
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanSmallDto>>> GetAll()
    {
        var loans = await _loanService.FindAllAsync();
        return Ok(loans);
    }

    // GET: /api/prestamos/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<LoanDto>> GetById(int id)
    {
        try
        {
            var loan = await _loanService.FindByIdAsync(id);
            return Ok(loan);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // GET: /api/prestamos/activos
    [HttpGet("activos")]
    public async Task<ActionResult<IReadOnlyList<LoanActiveDto>>> GetActive()
    {
        var activeLoans = await _loanService.FindActiveAsync();
        return Ok(activeLoans);
    }

    // GET: /api/prestamos/vencidos
    [HttpGet("vencidos")]
    public async Task<ActionResult<IReadOnlyList<LoanOverdueDto>>> GetOverdue()
    {
        var overdueLoans = await _loanService.FindOverdueAsync();
        return Ok(overdueLoans);
    }

    // POST: /api/prestamos
    [HttpPost]
    public async Task<ActionResult<LoanSmallDto>> Create([FromBody] LoanBodyDto dto)
    {
        var loan = await _loanService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
    }

    // PUT: /api/prestamos/{id}/devolver
    [HttpPut("{id}/devolver")]
    public async Task<ActionResult<LoanReturnDto>> ReturnBook(int id, [FromBody] LoanReturnDto dto)
    {
        try
        {
            var returned = await _loanService.ReturnAsync(id, dto);
            return Ok(returned);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // PUT: /api/prestamos/{id}/extender
    [HttpPut("{id}/extender")]
    public async Task<ActionResult<LoanSmallDto>> ExtendReturnDate(int id, [FromBody] LoanExtendDto dto)
    {
        try
        {
            var updated = await _loanService.ExtendAsync(id, dto);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}