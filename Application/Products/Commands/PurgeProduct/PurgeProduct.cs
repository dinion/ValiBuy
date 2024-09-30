using Application.Common.Interfaces;

namespace Application.Products.Commands.PurgeProduct
{
    public record PurgeProductCommand : IRequest;

    public class PurgeProductCommandHandler : IRequestHandler<PurgeProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurgeProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(PurgeProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Products.RemoveRange(await _unitOfWork.Products.GetAllAsync(), cancellationToken);
        }
    }
}