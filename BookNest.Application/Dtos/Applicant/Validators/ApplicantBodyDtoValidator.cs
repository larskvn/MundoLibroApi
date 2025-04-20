namespace BookNest.Application.Dtos.Applicant.Validators;

using FluentValidation;

public class ApplicantBodyDtoValidator:AbstractValidator<ApplicantBodyDto>
{
    public ApplicantBodyDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("El nombre completo es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre completo no puede exceder los 100 caracteres.");


        RuleFor(x => x.IdentityDocument)
            .NotEmpty().WithMessage("El documento de identidad es obligatorio.")
            .MaximumLength(20).WithMessage("El documento de identidad no puede exceder los 20 caracteres.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.")
            .MaximumLength(150).WithMessage("El email no puede exceder los 150 caracteres.");

  
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El teléfono es obligatorio.")
            .Matches(@"^\d{1,10}$").WithMessage("El teléfono debe tener hasta 10 dígitos numéricos.");
    }
    
}