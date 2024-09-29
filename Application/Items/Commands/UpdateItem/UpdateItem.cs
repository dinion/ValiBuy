using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Items.Commands.CreateItem;

public record UpdateItemCommand : IRequest
{
    public int ItemId { get; set; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Items
            .GetByIdAsync(request.ItemId, cancellationToken);

        Guard.Against.NotFound(request.ItemId, entity);

        await _unitOfWork.CompleteAsync();
    }
}