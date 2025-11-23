using BugStore.Api.Services;
using BugStore.Api.Services.Interface;
using BugStore.Handlers.Customers;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Test.RepositoriesTests;

namespace BugStore.Test.HandlerTests.Customers
{
    public class GetCustomersHandlerTest
    {
        private readonly CustomerRepositoryTest _repo = new CustomerRepositoryTest();
        private readonly ICustomerService _service;

        public GetCustomersHandlerTest()
        {
            _service = new CustomerService(_repo);
        }

        [Fact]
        public async Task Should_Return_List_Of_Customers()
        {
            //Arrange
            var fakeCustomer1 = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "MatheusDevBack",
                Email = "math",
                Phone = "123456t",
                BirthDate = new DateTime(1995, 11, 5)
            };
            var fakeCustomer2 = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "MatheusdevFront",
                Email = "math",
                Phone = "123456t",
                BirthDate = new DateTime(1995, 10, 5)
            };

            await _repo.AddAsync(fakeCustomer1, CancellationToken.None);
            await _repo.AddAsync(fakeCustomer2, CancellationToken.None);

            var testHandler = new GetCustomersHandler(_service);

            //Act
            var testResponse = await testHandler.Handle(new GetCustomerRequest(), CancellationToken.None);

            //Assert
            Assert.NotNull(testResponse);
            Assert.Equal("A Lista de clientes foi encontrada!", testResponse.Message);
            Assert.Contains(testResponse.Customer, c => c.Id == fakeCustomer1.Id);
            Assert.Contains(testResponse.Customer, c => c.Id == fakeCustomer2.Id);
            Assert.Equal(2, testResponse.Customer.Count);
        }

        [Fact]
        public async Task Should_Throw_ExceptionMessage_When_DoNot_Have_Customers()
        {
            //Arrange
            var testHandler = new GetCustomersHandler(_service);

            //Act e Assert
            var testResponseException = await Assert.ThrowsAsync<InvalidOperationException>(() 
                    => testHandler.Handle(new GetCustomerRequest(), CancellationToken.None));

            Assert.Equal("Nenhum Cliente Cadastrado", testResponseException.Message);
        }
    }
}
