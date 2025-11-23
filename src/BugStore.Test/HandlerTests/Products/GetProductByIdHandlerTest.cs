using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Products;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Products
{
    public class GetProductByIdHandlerTest
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IProductService _productService;

        public GetProductByIdHandlerTest()
        {
            _productRepo = new ProductRepositoryTest();
            _productService = new ProductService(_productRepo);
        }

        [Fact]
        public async Task Should_Get_Product_ById_Successfully()
        {
            // arrange
            var fakeProduct = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Ipad",
                Description = "Ipag ultima geração 2026",
                Slug = "ipad_5.5",
                Price = 5500
            };
            await _productRepo.AddAsync(fakeProduct, CancellationToken.None);

            var request = new GetProductByIdRequest(fakeProduct.Id);
            var handler = new GetProductByIdHandler(_productService);

            // act
            var response = await handler.Handle(request, CancellationToken.None);

            // assert
            Assert.NotNull(response);
            Assert.Equal(fakeProduct.Id, response.Product.Id);
            Assert.Equal(fakeProduct.Title, response.Product.Title);
        }

        [Fact]
        public async Task Should_Send_ExceptionMessage_When_ProductId_NotFound()
        {
            // arrange
            var fakeId = Guid.NewGuid();

            var request = new GetProductByIdRequest(fakeId);
            var handler = new GetProductByIdHandler(_productService);

            // act & assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(request, CancellationToken.None));

            Assert.Contains("Produto não encontrado!", exception.Message);
        }
    }
}
