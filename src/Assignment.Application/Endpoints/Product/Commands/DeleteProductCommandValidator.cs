using FluentValidation;

namespace Assignment.Application.Endpoints.Product.Commands;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
    }
}
