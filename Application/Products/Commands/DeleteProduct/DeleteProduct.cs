using Application.Common.Interfaces;

namespace Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(int productId) : IRequest;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(command.productId, cancellationToken);

            Guard.Against.NotFound(command.productId, product);

            await _unitOfWork.Products.RemoveAsync(product);
        }
    }
}