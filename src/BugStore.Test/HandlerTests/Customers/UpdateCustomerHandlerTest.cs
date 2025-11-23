using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Customers;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Customers
{
    public class UpdateCustomerHandlerTest
    {
        private readonly CustomerRepositoryTest _repo = new CustomerRepositoryTest();
        private readonly ICustomerService _service;

        public UpdateCustomerHandlerTest()
        {
            _service = new CustomerService(_repo);
        }

        [Fact]
        public async Task Should_Update_Customer_Successfully()
        {
            //arrange
            var fakeCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "MatheuMAntovan",
                Email = "math",
                Phone = "123456t",
                BirthDate = new DateTime(1995, 10, 5)
            };
            await _repo.AddAsync(fakeCustomer, CancellationToken.None);

            var testRequest = new UpdateCustomerRequest
            (
                "Matheus Silva",
                "MAth@gmail.com",
                "1140028922",
                new DateTime(2001, 08, 25)
            );

            var testCommand = new UpdateCustomerCommand(fakeCustomer.Id, testRequest);

            var updateHandlerTest = new UpdateCustomerHandler(_service);

            //act
            var testResponse = await updateHandlerTest.Handle(testCommand, CancellationToken.None);

            //assert
            Assert.NotNull(testResponse);
            Assert.Contains("Cliente atualizado com sucesso!", testResponse.Message);
            var updatedCustomer = await _repo.GetByIdAsync(fakeCustomer.Id, CancellationToken.None);
            Assert.Equal("Matheus Silva", updatedCustomer.Name);
            Assert.Equal("MAth@gmail.com", updatedCustomer.Email);
            Assert.Equal("1140028922", updatedCustomer.Phone);
            Assert.Equal(new DateTime(2001, 08, 25), updatedCustomer.BirthDate);
        }

        [Fact]
        public async Task Should_Send_ExceptionMessage_When_CustomerId_Is_Not_Found()
        {
            //arrange
            var fakeCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "MatheuMAntovan",
                Email = "math",
                Phone = "123456t",
                BirthDate = new DateTime(1995, 10, 5)
            };
            await _repo.AddAsync(fakeCustomer, CancellationToken.None);

            var testRequest = new UpdateCustomerRequest
            (
                "Matheus Silva",
                "MAth@gmail.com",
                "1140028922",
                new DateTime(2001, 08, 25)
            );
            var wrongId = Guid.NewGuid();

            var testCommand = new UpdateCustomerCommand(wrongId, testRequest);

            var updateHandlerTest = new UpdateCustomerHandler(_service);

            //act and assert
            var testResponse = await Assert.ThrowsAsync<ArgumentException>(() => updateHandlerTest.Handle(testCommand, CancellationToken.None));

            Assert.Equal("Cliente não encontrado para ser atualizado!", testResponse.Message);
        }
    }
}