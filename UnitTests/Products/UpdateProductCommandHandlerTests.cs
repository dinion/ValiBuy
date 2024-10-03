using Application.Common.Interfaces;
using Application.Products.Commands.UpdateProduct;
using Ardalis.GuardClauses;
using Domain.Entities;
using Moq;

namespace UnitTests.Products
{
    [TestFixture]
    public class UpdateProductCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private UpdateProductCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateProductCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldUpdateProduct_WhenProductExists()
        {
            var command = new UpdateProductCommand { Id = 1, Name = "Updated Product", Price = 19.99m };
            var existingProduct = new Product { Id = 1, Name = "Old Product", Price = 10.00m };

            _unitOfWorkMock.Setup(uow => uow.Products.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingProduct);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.AreEqual("Updated Product", existingProduct.Name);
            Assert.AreEqual(19.99m, existingProduct.Price);
            _unitOfWorkMock.Verify(uow => uow.Products.UpdateAsync(existingProduct), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            var command = new UpdateProductCommand { Id = 2, Name = "Updated Product", Price = 19.99m };

            _unitOfWorkMock.Setup(uow => uow.Products.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product)null); // Simulate not found

            Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}