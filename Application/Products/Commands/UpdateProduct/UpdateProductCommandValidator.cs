namespace Application.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Validator for <see cref="UpdateProductCommand"/> to ensure valid product updates.
    /// </summary>
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandValidator"/> class.
        /// </summary>
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