using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Orders;
using BugStore.Models;
using BugStore.Requests.Orders;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Orders
{
    public class CreateOrderHandlerTest
    {
        private readonly IRepository<Order> _orderRepo = new OrderRepositoryTest();
        private readonly IRepository<Customer> _customerRepo = new CustomerRepositoryTest();
        private readonly IRepository<Product> _productRepo = new ProductRepositoryTest();
            
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CreateOrderHandlerTest()
        {
            _customerService = new CustomerService(_customerRepo);
            _productService = new ProductService(_productRepo);
            _orderService = new OrderService(_orderRepo, _productService, _customerService);
        }

        [Fact]
        public async Task Should_Create_Order_Successfully()
        {
            // arrange 
            var customer = await _customerService.CreateCustomerAsync(
                "Cliente Teste",
                "cliente@teste.com",
                "11999999999",
                new DateTime(1990, 1, 1),
                CancellationToken.None);

            var product = await _productService.CreateProductAsync(
                "Produto Teste",
                "Descrição produto teste",
                "produto-teste",
                100m,
                CancellationToken.None);

            var request = new CreateOrderRequest
                (
                    customer.Id,
                    new List<CreateOrderLineRequest>
                        {
                            new(product.Id, 2)
                        }
                );

            var handler = new CreateOrderHandler(_orderService);

            // act
            var response = await handler.Handle(request, CancellationToken.None);

            // assert
            Assert.NotNull(response);
            Assert.Contains("Ordem criada com sucesso!", response.Message);

            var created = await _orderRepo.GetByIdAsync(response.Id, CancellationToken.None);
            Assert.NotNull(created);
            Assert.Equal(customer.Id, created.CustomerId);
            Assert.NotNull(created.Lines);
            Assert.Single(created.Lines);
            Assert.Equal(product.Id, created.Lines[0].ProductId);
            Assert.Equal(2, created.Lines[0].Quantity);
            Assert.Equal(product.Price * 2, created.Lines[0].Total);
        }

        [Fact]
        public async Task Should_Throw_When_Customer_Not_Found()
        {
            // arrange 
            var product = await _productService.CreateProductAsync(
                "Produto Teste 2",
                "Descrição",
                "produto-teste-2",
                50m,
                CancellationToken.None);

            var request = new CreateOrderRequest(Guid.NewGuid(), new List<CreateOrderLineRequest>
            {
                new(product.Id, 1)
            });

            var handler = new CreateOrderHandler(_orderService);

            // act & assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(request, CancellationToken.None));
            Assert.Equal("Cliente não encontrado!", ex.Message);
        }
    }
}
