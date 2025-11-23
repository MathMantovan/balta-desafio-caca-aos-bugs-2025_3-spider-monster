using BugStore.Api.Requests.Customers;
using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Customers;
using BugStore.Models;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Customers
{
    public class CreateCustomerHandlerTests
    {
        private readonly CustomerRepositoryTest _repo = new CustomerRepositoryTest();
        private readonly ICustomerService _service;

        public CreateCustomerHandlerTests()
        {
            _service = new CustomerService(_repo);
        }

        [Fact]
        public async Task Should_Create_Customer_Successfully()
        {
            // Arrange
            var testRequest = new CreateCustomerRequest
            (
                "Matheus Mantovan",
                "matheus@email.com",
                "11999999999",
                new DateTime(1995, 10, 5)
            );

            var createHandlerTest = new CreateCustomerHandler(_service);

            // Act
            var testeResponse = await createHandlerTest.Handle(testRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(testeResponse);
            Assert.Contains("Cliente criado com sucesso!", testeResponse.Message);
        }

        [Fact]
        public async Task Should_Not_Create_When_Customer_Already_Exist()
        {
            // Arrange
            var fakeCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "MatheuMAntovan",
                Email = "math",
                Phone = "123456t",
                BirthDate = new DateTime(1995, 10, 5)
            };
            await _repo.AddAsync(fakeCustomer, CancellationToken.None);

            var testRequest = new CreateCustomerRequest
            (
                "MatheuMAntovan",
                "matheus@email.com",
                "11999999999",
                new DateTime(1995, 10, 5)
            );

            var createHandlerTest = new CreateCustomerHandler(_service);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                createHandlerTest.Handle(testRequest, CancellationToken.None));

            Assert.Equal("Os dados deste cliente ja estão cadastrados!", exception.Message);
        }

        [Fact]
        public async Task Should_Not_Create_When_Name_Is_Null()
        {
            // Arrange
            var testRequest = new CreateCustomerRequest
            (
                null,
                "email@email.com",
                "1111111111",
                new DateTime(1995, 10, 5)
            );

            var createHandlerTest = new CreateCustomerHandler(_service);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                createHandlerTest.Handle(testRequest, CancellationToken.None));

            Assert.Equal("Nome é obrigatório.", exception.Message);
        }

        [Fact]
        public async Task Should_Not_Create_When_Email_Is_Null()
        {
            // Arrange
            var testRequest = new CreateCustomerRequest
            (
                "matheus",
                null,
                "1111111111",
                new DateTime(1995, 10, 5)
            );

            var createHandlerTest = new CreateCustomerHandler(_service);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                createHandlerTest.Handle(testRequest, CancellationToken.None));

            Assert.Equal("Email é obrigatório.", exception.Message);
        }

        [Fact]
        public async Task Should_Not_Create_When_BirthDate_Is_Null()
        {
            // Arrange
            var testRequest = new CreateCustomerRequest
            (
                "matheus",
                "math@m",
                "1111111111",
                new DateTime(2030, 10, 5)
            );

            var createHandlerTest = new CreateCustomerHandler(_service);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                createHandlerTest.Handle(testRequest, CancellationToken.None));

            Assert.Equal("Data de nascimento incorreta", exception.Message);
        }
    }
}
