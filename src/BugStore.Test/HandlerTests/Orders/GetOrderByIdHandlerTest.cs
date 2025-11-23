using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Orders;
using BugStore.Models;
using BugStore.Requests.Orders;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Orders
{
    public class GetOrderByIdHandlerTest
    {
        private readonly IRepository<Order> _orderRepo = new OrderRepositoryTest();
        private readonly IRepository<Customer> _customerRepo = new CustomerRepositoryTest();
        private readonly IRepository<Product> _productRepo = new ProductRepositoryTest();

        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public GetOrderByIdHandlerTest()
        {
            _customerService = new CustomerService(_customerRepo);
            _productService = new ProductService(_productRepo);
            _orderService = new OrderService(_orderRepo, _productService, _customerService);
        }

        [Fact]
        public async Task Should_Get_Order_ById_Successfully()
        {
            // arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Produto Teste",
                Description = "Desc",
                Slug = "produto-teste",
                Price = 50m
            };
            await _productRepo.AddAsync(product, CancellationToken.None);

            var fakeOrder = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Lines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        OrderId = Guid.Empty, 
                        ProductId = product.Id,
                        Product = product,
                        Quantity = 2,
                        Total = 100m
                    }
                }
            };
            
            foreach (var line in fakeOrder.Lines)
                line.OrderId = fakeOrder.Id;

            await _orderRepo.AddAsync(fakeOrder, CancellationToken.None);

            var request = new GetOrderByIdRequest(fakeOrder.Id);
            var handler = new GetOrderByIdHandler(_orderService);

            // act
            var response = await handler.Handle(request, CancellationToken.None);

            // assert
            Assert.NotNull(response);
            Assert.Equal(fakeOrder.Id, response.Order.Id);
            Assert.Equal(fakeOrder.CustomerId, response.Order.CustomerId);
            Assert.Equal("Ordem encontrada com sucesso!", response.message);
        }

        [Fact]
        public async Task Should_Send_Exception_When_OrderId_NotFound()
        {
            // arrange
            var nonExistentId = Guid.NewGuid();
            var request = new GetOrderByIdRequest(nonExistentId);
            var handler = new GetOrderByIdHandler(_orderService);

            // act & assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(request, CancellationToken.None));
            Assert.Equal("Ordem não encontrada", ex.Message);
        }
    }
}
