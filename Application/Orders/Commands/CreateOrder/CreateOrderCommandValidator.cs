namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Items cannot be null.")
            .Must(items => items.Count > 0).WithMessage("At least one item is required.")
            .ForEach(item => item
                .Must(i => i.Quantity > 0).WithMessage("Quantity must be greater than 0.")
            );
    }
}