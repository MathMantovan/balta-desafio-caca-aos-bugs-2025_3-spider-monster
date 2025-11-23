using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Commands.Products;
using BugStore.Handlers.Products;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Products
{
    public class UpdateProductHandlerTest
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IProductService _productService;

        public UpdateProductHandlerTest()
        {
            _productRepo = new ProductRepositoryTest();
            _productService = new ProductService(_productRepo);
        }

        [Fact]
        public async Task Should_Update_Product_Successfully()
        {
            // arrange
            var fakeProduct = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Ipad",
                Description = "Ipad antiga",
                Slug = "ipad_old",
                Price = 5000
            };
            await _productRepo.AddAsync(fakeProduct, CancellationToken.None);

            var updateRequest = new UpdateProductRequest(
                "Ipad Atualizado",
                "Nova descrição",
                "ipad_new",
                5500
            );

            var command = new UpdateProductCommand(fakeProduct.Id, updateRequest);
            var handler = new UpdateProductHandler(_productService);

            // act
            var response = await handler.Handle(command, CancellationToken.None);

            // assert
            Assert.NotNull(response);
            Assert.Contains("O Produto foi atualizado com sucesso!", response.Message);

            var updated = await _productRepo.GetByIdAsync(fakeProduct.Id, CancellationToken.None);
            Assert.Equal("Ipad Atualizado", updated.Title);
            Assert.Equal("Nova descrição", updated.Description);
            Assert.Equal("ipad_new", updated.Slug);
            Assert.Equal(5500m, updated.Price);
        }

        [Fact]
        public async Task Should_Send_ExceptionMessage_When_ProductId_Is_Not_Found()
        {
            // arrange
            var fakeProduct = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Produto X",
                Description = "Desc",
                Slug = "produto_x",
                Price = 100
            };
            await _productRepo.AddAsync(fakeProduct, CancellationToken.None);

            var updateRequest = new UpdateProductRequest(
                "Novo",
                "Nova desc",
                "novo_slug",
                200m
            );

            var wrongId = Guid.NewGuid();
            var command = new UpdateProductCommand(wrongId, updateRequest);
            var handler = new UpdateProductHandler(_productService);

            // act & assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal("Produto não encontrado para ser atualizado!", exception.Message);
        }
    }
}
