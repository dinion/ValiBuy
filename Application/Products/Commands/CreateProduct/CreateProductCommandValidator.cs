namespace Application.Products.Commands.CreateProduct;

/// <summary>
/// Validator for <see cref="CreateProductCommand"/> to ensure proper data validation.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductCommandValidator"/> class.
    /// </summary>
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is mandatory!");

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Price cannot be 0 or less!");
    }
}