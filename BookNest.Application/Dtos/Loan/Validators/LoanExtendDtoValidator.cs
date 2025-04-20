namespace BookNest.Application.Dtos.Loan.Validators;

using FluentValidation;

public class LoanExtendDtoValidator: AbstractValidator<LoanExtendDto>
{
    public LoanExtendDtoValidator()
    {
        RuleFor(x => x.NewReturnDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("La nueva fecha de devolución debe ser posterior a hoy.");
    }
    
}