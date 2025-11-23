using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Customers;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Customers
{
    public class DeleteCustomerHandlerTest
    {
        private readonly CustomerRepositoryTest _repo = new CustomerRepositoryTest();
        private readonly ICustomerService _service;

        public DeleteCustomerHandlerTest()
        {
            _service = new CustomerService(_repo);
        }

        [Fact]
        public async Task Should_Delete_Customer_Successfully()
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

            var testRequest = new DeleteCustomerRequest(fakeCustomer.Id);

            var deleteHandlerTest = new DeleteCustomerHandler(_service);

            //act
            var testResponse = await deleteHandlerTest.Handle(testRequest, CancellationToken.None);

            //assert
            Assert.NotNull(testResponse);
            Assert.Contains("foi deletado com sucesso", testResponse.Message);
            Assert.Equal(fakeCustomer.Id, testResponse.Id);
            var DeletedCustomer = await _repo.GetByIdAsync(fakeCustomer.Id, CancellationToken.None);
            Assert.Null(DeletedCustomer);
        }

        [Fact]
        public async Task Should_Send_ExceptionMessage_When_CustomerId_Is_Not_Found()
        {
            //arrange
            var fakeId = Guid.NewGuid();

            var testRequest = new DeleteCustomerRequest(fakeId);

            var deleteHandlerTest = new DeleteCustomerHandler(_service);

            //act and assert
            var testResponse = await Assert.ThrowsAsync<ArgumentException>(() => deleteHandlerTest.Handle(testRequest, CancellationToken.None));

            Assert.Equal("Cliente não encontrado!", testResponse.Message);
        }
    }
}
