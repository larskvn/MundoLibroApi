namespace BookNest.Application.Dtos.Loan.Validators;

using FluentValidation;

public class LoanBodyDtoValidator:AbstractValidator<LoanBodyDto>
{
    public LoanBodyDtoValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("El ID del solicitante debe ser mayor que 0.");

        RuleFor(x => x.ReturnDate)
            .Must(date => date == null || date.Value.Date >= DateTime.UtcNow.Date)
            .WithMessage("La fecha de devolución no puede ser anterior a hoy."); 
    }
}