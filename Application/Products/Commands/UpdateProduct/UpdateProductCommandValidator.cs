namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product Id is mandatory!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is mandatory!");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price cannot be 0 or less!");

        }
    }
}