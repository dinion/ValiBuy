using Application.Common.Interfaces;
using Application.Orders.Queries.GetCustomerOrders;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Application.Tests.Orders.Queries
{
    [TestFixture]
    public class GetCustomerOrdersQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private GetCustomerOrdersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetCustomerOrdersQueryHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnOrders_WhenCustomerIdIsValid()
        {
            var query = new GetCustomerOrdersQuery
            {
                CustomerId = 1,
                StartDate = DateTime.Today.AddDays(-10),
                EndDate = DateTime.Today
            };

            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Today.AddDays(-5),
                    TotalPrice = 100m,
                    OrderItems = new List<Item>
                    {
                        new Item { Id = 1, ProductId = 1, Quantity = 2 }
                    }
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Today.AddDays(-2),
                    TotalPrice = 200m,
                    OrderItems = new List<Item>
                    {
                        new Item { Id = 2, ProductId = 2, Quantity = 1 }
                    }
                }
            };

            _unitOfWorkMock.Setup(x => x.Orders.GetAllAsync(It.IsAny<Func<IQueryable<Order>, IQueryable<Order>>>()))
                .ReturnsAsync(orders);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().HaveCount(2);
            result.Should().ContainSingle(o => o.OrderId == 1);
            result.Should().ContainSingle(o => o.OrderId == 2);
        }

        [Test]
        public async Task Handle_ShouldReturnOrders_WhenSpecificDateIsProvided()
        {
            var query = new GetCustomerOrdersQuery
            {
                CustomerId = 1,
                SpecificDate = DateTime.Today.AddDays(-5)
            };

            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Today.AddDays(-5),
                    TotalPrice = 100m,
                    OrderItems = new List<Item>()
                }
            };

            _unitOfWorkMock.Setup(x => x.Orders.GetAllAsync(It.IsAny<Func<IQueryable<Order>, IQueryable<Order>>>()))
                .ReturnsAsync(orders);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().HaveCount(1);
            result.Should().ContainSingle(o => o.OrderId == 1);
        }

        [Test]
        public async Task Handle_ShouldReturnNoOrders_WhenNoOrdersMatchCriteria()
        {
            var query = new GetCustomerOrdersQuery
            {
                CustomerId = 1,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(10)
            };

            var orders = new List<Order>();

            _unitOfWorkMock.Setup(x => x.Orders.GetAllAsync(It.IsAny<Func<IQueryable<Order>, IQueryable<Order>>>()))
                .ReturnsAsync(orders);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().BeEmpty();
        }
    }
}
