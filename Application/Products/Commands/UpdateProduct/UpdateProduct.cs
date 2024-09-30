using Application.Common.Interfaces;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(command.Id, cancellationToken);
            Guard.Against.NotFound(command.Id, product);

            product.Name = command.Name;
            product.Price = command.Price;

            await _unitOfWork.Products.UpdateAsync(product);

            return true;
        }
    }
}