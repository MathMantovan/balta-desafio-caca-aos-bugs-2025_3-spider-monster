using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Products;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Products
{
    public class CreateProductHandlerTest
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IProductService _productService;

        public CreateProductHandlerTest()
        {
            _productRepo = new ProductRepositoryTest();
            _productService = new ProductService(_productRepo);
        }

        [Fact]
        public async Task Should_Create_Product_Successfully()
        {
            // arrange
            var request = new CreateProductRequest
            (
                "Ipad",
                "Ipag ultima geração 2026",
                "ipad_5.5",
                5500
            );

            var createHandler = new CreateProductHandler(_productService);

            // act
            var response = await createHandler.Handle(request, CancellationToken.None);

            // assert
            Assert.NotNull(response);
            Assert.Contains("Produto criado com sucesso!", response.Message);
        }

        [Fact]
        public async Task Should_Not_Create_When_Product_Already_Exist()
        {
            // Arrange
            var fakeProduct = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Ipad",
                Description = "Descrição antiga",
                Slug = "ipad_5.5",
                Price = 5500
            };
            await _productRepo.AddAsync(fakeProduct, CancellationToken.None);

            var request = new CreateProductRequest
            (
                "Ipad",
                "Nova descrição",
                "ipad_5.6",
                6000
            );

            var createHandler = new CreateProductHandler(_productService);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                createHandler.Handle(request, CancellationToken.None));

            Assert.Equal("Os dados deste produto ja estão cadastrados!", exception.Message);
        }

        [Fact]
        public async Task Should_Not_Create_When_Title_Is_Null()
        {
            // Arrange
            var request = new CreateProductRequest
            (
                null,
                "Descrição",
                "slug_teste",
                100
            );

            var createHandler = new CreateProductHandler(_productService);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                createHandler.Handle(request, CancellationToken.None));

            Assert.Equal("Titulo é obrigatório.", exception.Message);
        }

        [Fact]
        public async Task Should_Not_Create_When_Slug_Is_Null()
        {
            // Arrange
            var request = new CreateProductRequest
            (
                "Produto X",
                "Descrição",
                null,
                100
            );

            var createHandler = new CreateProductHandler(_productService);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                createHandler.Handle(request, CancellationToken.None));

            Assert.Equal("O Slug é obrigatório", exception.Message);
        }

        [Fact]
        public async Task Should_Not_Create_When_Price_Is_Invalid()
        {
            // Arrange
            var request = new CreateProductRequest
            (
                "Produto Y",
                "Descrição",
                "produto-y",
                0 // preço inválido
            );

            var createHandler = new CreateProductHandler(_productService);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                createHandler.Handle(request, CancellationToken.None));

            Assert.Equal("Preço inválido.", exception.Message);
        }
    }
}