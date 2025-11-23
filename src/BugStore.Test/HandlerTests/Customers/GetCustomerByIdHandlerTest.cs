using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Customers;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Customers
{
    public class GetCustomerByIdHandlerTest
    {
        private readonly CustomerRepositoryTest _repo = new CustomerRepositoryTest();
        private readonly ICustomerService _service;

        public GetCustomerByIdHandlerTest()
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
                Name = "MatheuDev",
                Email = "math",
                Phone = "123456t",
                BirthDate = new DateTime(1995, 10, 5)
            };
            await _repo.AddAsync(fakeCustomer, CancellationToken.None);

            var testRequest = new GetCustomerByIdRequest(fakeCustomer.Id);

            var GetByIdHandler = new GetCustomerByIdHandler(_service);

            //act
            var testResponse = await GetByIdHandler.Handle(testRequest, CancellationToken.None);

            //assert
            Assert.NotNull(testResponse);
            Assert.Equal(testResponse.Customer.Name, fakeCustomer.Name);
            Assert.Equal(testResponse.Customer.Id, fakeCustomer.Id);
        }

        [Fact]
        public async Task Should_Send_ExceptionMessage_Whent_CustomerId_NotFound()
        {
            //arrange
            var fakeId = Guid.NewGuid();

            var testRequest = new GetCustomerByIdRequest(fakeId);

            var GetByIdHandler = new GetCustomerByIdHandler(_service);

            //act and Assert
            var testResponseException =  await Assert.ThrowsAsync<ArgumentException>(() => GetByIdHandler.Handle(testRequest, CancellationToken.None));

            Assert.Contains("Cliente não encontrado!", testResponseException.Message);
        }
    }
}
