namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
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
}