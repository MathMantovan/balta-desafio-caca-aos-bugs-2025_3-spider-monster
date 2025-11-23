using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services.Interface;
using BugStore.Models;
using BugStore.Requests.Orders;

namespace BugStore.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _OrderRepository;
        private readonly IProductService _productService;
        private readonly ICustomerService  _customerService;
        public OrderService(IRepository<Order> orderRepository, IProductService product, ICustomerService customerService)
        {
            _OrderRepository = orderRepository;
            _productService = product;
            _customerService = customerService;
        }
        public async Task<Order> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetCustomerByIdAsync(request.CustomerId, cancellationToken);

            if (customer == null)
                throw new ArgumentException("Customer não encontrado");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Lines = new List<OrderLine>()
            };

            foreach(var line in request.Lines)
            {
                var product = await _productService.GetProductByIdAsync(line.ProductId, cancellationToken);

                if (product == null) continue;                    

                var orderLine = new OrderLine
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    Quantity = line.Quantity,
                    Total = line.Quantity * product.Price,
                    ProductId = line.ProductId,
                    Product = product
                };
                order.Lines.Add(orderLine);
            }

            await _OrderRepository.AddAsync(order, cancellationToken);
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _OrderRepository.GetByIdAsync(id, cancellationToken);
            
            if(order is null)
                throw new ArgumentException("Ordem não encontrada");

            return order;
        }
    }
}
