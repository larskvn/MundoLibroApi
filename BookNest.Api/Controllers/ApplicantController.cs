using BookNest.Application.Dtos.Applicant;
using BookNest.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/solicitantes")]
public class ApplicantController : ControllerBase
{
    private readonly IApplicantService _applicantService;

    public ApplicantController(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    // GET: /api/solicitantes
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ApplicantDto>>> GetAll()
    {
        var applicants = await _applicantService.FindAllAsync();
        return Ok(applicants);
    }

    // GET: /api/solicitantes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicantDto>> GetById(int id)
    {
        try
        {
            var applicant = await _applicantService.FindByIdAsync(id);
            return Ok(applicant);
        }
        catch (Exception ex) when (ex.Message.Contains("no encontrado"))
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message });
        }
    }

    // GET: /api/solicitantes/{id}/prestamos
    [HttpGet("{id}/prestamos")]
    public async Task<ActionResult<ApplicantWithLoansDto>> GetLoansByApplicant(int id)
    {
        try
        {
            var applicantWithLoans = await _applicantService.FindWithLoansAsync(id);
            return Ok(applicantWithLoans);
        }
        catch (Exception ex) when (ex.Message.Contains("no encontrado"))
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message });
        }
    }

    // POST: /api/solicitantes
    [HttpPost]
    public async Task<ActionResult<ApplicantDto>> Create([FromBody] ApplicantBodyDto dto)
    {
        try
        {
            var created = await _applicantService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (FluentValidation.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(new { errors });
        }
        catch (Exception ex)
        {
           
            return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
        }
    }

    // PUT: /api/solicitantes/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<ApplicantDto>> Update(int id, [FromBody] ApplicantBodyDto dto)
    {
        try
        {
            var updated = await _applicantService.UpdateAsync(id, dto);
            return Ok(updated);
        }
        catch (FluentValidation.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new { campo = e.PropertyName, mensaje = e.ErrorMessage });
            return BadRequest(new { errores = errors });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message });
        }
    }
}