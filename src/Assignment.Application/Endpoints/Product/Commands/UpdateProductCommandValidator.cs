using FluentValidation;

namespace Assignment.Application.Endpoints.Product.Commands;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 50);

        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(1, 500);
    }
}
