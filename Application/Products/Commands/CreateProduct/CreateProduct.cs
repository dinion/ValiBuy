using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Name = command.Name,
                Price = command.Price,
            };

            await _unitOfWork.Products.AddAsync(entity);

            return entity.Id;
        }
    }
}
