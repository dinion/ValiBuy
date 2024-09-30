namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("FirstName is required!")
                .MaximumLength(255).WithMessage("Max length of name is 255 characters!");

            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("LastName is required!")
                .MaximumLength(255).WithMessage("Max length of lastName is 255 characters!");

            RuleFor(x => x.Address)
                .NotEmpty().NotNull().WithMessage("Address is required!");

            RuleFor(x => x.PostalCode)
                .NotEmpty().NotNull().WithMessage("PostalCode is required")
                .MaximumLength(10).WithMessage("PostalCode cannot exceed 10 characters!");
        }
    }
}