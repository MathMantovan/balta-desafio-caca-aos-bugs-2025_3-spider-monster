using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Products;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Products
{
    public class GetProductsHandlerTest
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IProductService _productService;

        public GetProductsHandlerTest()
        {
            _productRepo = new ProductRepositoryTest();
            _productService = new ProductService(_productRepo);
        }

        [Fact]
        public async Task Should_Return_List_Of_Products()
        {
            // arrange
            var fakeProduct1 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Ipad Pro",
                Description = "Descrição 1",
                Slug = "ipad_pro",
                Price = 6000
            };
            var fakeProduct2 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Ipad Mini",
                Description = "Descrição 2",
                Slug = "ipad_mini",
                Price = 3500
            };

            await _productRepo.AddAsync(fakeProduct1, CancellationToken.None);
            await _productRepo.AddAsync(fakeProduct2, CancellationToken.None);

            var handler = new GetProductHandler(_productService);

            // act
            var response = await handler.Handle(new GetProductRequest(), CancellationToken.None);

            // assert
            Assert.NotNull(response);
            Assert.Equal("A lista de produtos foi encontrada!", response.Message);
            Assert.Contains(response.Products, p => p.Id == fakeProduct1.Id);
            Assert.Contains(response.Products, p => p.Id == fakeProduct2.Id);
            Assert.Equal(2, response.Products.Count);
        }

        [Fact]
        public async Task Should_Throw_ExceptionMessage_When_DoNot_Have_Products()
        {
            // arrange
            var handler = new GetProductHandler(_productService);

            // act & assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(new GetProductRequest(), CancellationToken.None));

            Assert.Equal("Nenhum produto Cadastrado", exception.Message);
        }
    }
}
