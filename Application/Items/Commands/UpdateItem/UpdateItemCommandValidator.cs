using FluentValidation;

namespace Application.Items.Commands.CreateItem;

public class UpdateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        //RuleFor
    }
}