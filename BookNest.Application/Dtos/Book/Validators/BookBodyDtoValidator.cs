namespace BookNest.Application.Dtos.Book.Validators;

using FluentValidation;

public class BookBodyDtoValidator: AbstractValidator<BookBodyDto>
{
    public BookBodyDtoValidator()
    {
        RuleFor(x => x.Isbn)
            .NotEmpty().WithMessage("El ISBN es obligatorio.")
            .Length(10, 13).WithMessage("El ISBN debe tener entre 10 y 13 caracteres.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es obligatorio.")
            .MaximumLength(150).WithMessage("El título no debe exceder los 150 caracteres.");

        RuleFor(x => x.Authors)
            .NotEmpty().WithMessage("El autor son obligatorios.")
            .MaximumLength(100).WithMessage("El campo de autores no debe exceder los 100 caracteres.");

        RuleFor(x => x.Edition)
            .NotEmpty().WithMessage("La edición es obligatoria.")
            .MaximumLength(50).WithMessage("La edición no debe exceder los 50 caracteres.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1500, DateTime.Now.Year)
            .WithMessage($"El año debe estar entre 1500 y {DateTime.Now.Year}.");

        RuleFor(x => x.EditorialId)
            .GreaterThan(0).WithMessage("Debe especificar una editorial válida.");
    }
    
}