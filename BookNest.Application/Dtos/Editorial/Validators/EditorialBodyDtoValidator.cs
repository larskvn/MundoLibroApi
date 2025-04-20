namespace BookNest.Application.Dtos.Editorial.Validators;

using FluentValidation;

public class EditorialBodyDtoValidator:AbstractValidator<EditorialBodyDto>
{
    public EditorialBodyDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código de la editorial es obligatorio.")
            .Length(3, 20).WithMessage("El código debe tener entre 3 y 20 caracteres.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la editorial es obligatorio.")
            .Length(3, 100).WithMessage("El nombre debe tener entre 3 y 100 caracteres.");
    }
}