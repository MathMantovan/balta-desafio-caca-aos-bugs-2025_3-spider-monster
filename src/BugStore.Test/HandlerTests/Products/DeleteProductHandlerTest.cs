using BugStore.Api.Repositories.Interface;
using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Products;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Products
{
    public class DeleteProductHandlerTest
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IProductService _productService;

        public DeleteProductHandlerTest()
        {
            _productRepo = new ProductRepositoryTest();
            _productService = new ProductService(_productRepo);
        }

        [Fact]
        public async Task Should_Delete_Product_Successfully()
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

            var request = new DeleteProductRequest(fakeProduct.Id);
            var deleteHandler = new DeleteProductHandler(_productService);

            // act
            var response = await deleteHandler.Handle(request, CancellationToken.None);

            // assert
            Assert.NotNull(response);
            Assert.Contains("removido com sucesso", response.Message);
            Assert.Equal(fakeProduct.Id, response.Id);

            var deleted = await _productRepo.GetByIdAsync(fakeProduct.Id, CancellationToken.None);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task Should_Send_ExceptionMessage_When_ProductId_Is_Not_Found()
        {
            // arrange
            var fakeId = Guid.NewGuid();

            var request = new DeleteProductRequest(fakeId);
            var deleteHandler = new DeleteProductHandler(_productService);

            // act & assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                deleteHandler.Handle(request, CancellationToken.None));

            Assert.Equal("Produto não encontrado!", exception.Message);
        }
    }
}
