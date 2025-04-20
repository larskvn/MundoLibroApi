namespace BookNest.Application.Dtos.Loan.Validators;

using FluentValidation;

public class LoanReturnDtoValidator: AbstractValidator<LoanReturnDto>
{
    public LoanReturnDtoValidator()
    {
        RuleFor(x => x.LoanStatus)
            .Must(s => s == 1 || s == 2)
            .WithMessage("El estado del préstamo debe ser 1 (devuelto) o 2 (devuelto parcialmente).");

    }
    
}