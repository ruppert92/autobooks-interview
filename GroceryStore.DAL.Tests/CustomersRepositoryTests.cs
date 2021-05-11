using GroceryStore.DAL.Contexts;
using GroceryStore.DAL.Models;
using GroceryStore.DAL.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GroceryStore.DAL.Tests
{
    public class CustomersRepositoryTests
    {
        [Fact]
        public async Task Should_Get_Customer_By_Id()
        {
            var dbContextMock = new Mock<IGroceryStoreDbContext>();
            var customer = new Customer() { Id = 1, Name = "Test" };
            dbContextMock.Setup(c => c.GetById<Customer>(1, default)).Returns(Task.FromResult(customer));

            var repository = new GroceryStoryRepository<Customer>(dbContextMock.Object);
            var result = await repository.GetById(1, default);
            Assert.Equal(customer, result);
        }

        [Fact]
        public async Task Should_Get_All_Customers()
        {
            var dbContextMock = new Mock<IGroceryStoreDbContext>();
            var customers = new List<Customer>() { new Customer() { Id = 1, Name = "Test" }, new Customer() { Id = 2, Name = "Test2" } };
            dbContextMock.Setup(c => c.GetAll<Customer>(default)).Returns(Task.FromResult((IEnumerable<Customer>)customers));

            var repository = new GroceryStoryRepository<Customer>(dbContextMock.Object);
            var result = await repository.GetAll(default);
            Assert.Equal(customers, result);
        }

        [Fact]
        public async Task Should_Create_Customer()
        {
            var dbContextMock = new Mock<IGroceryStoreDbContext>();
            var customer = new Customer() { Name = "Test" };
            var newCustomer = new Customer() { Name = "Test" };
            dbContextMock.Setup(c => c.Add<Customer>(customer, default)).Returns(Task.FromResult(newCustomer));

            var repository = new GroceryStoryRepository<Customer>(dbContextMock.Object);
            var result = await repository.Add(customer, default);
            Assert.Equal(newCustomer, result);
        }

        [Fact]
        public async Task Should_Update_Customer()
        {
            var dbContextMock = new Mock<IGroceryStoreDbContext>();
            var customer = new Customer() { Id = 1, Name = "Test3" };
            dbContextMock.Setup(c => c.Update<Customer>(customer, default)).Returns(Task.CompletedTask);

            var repository = new GroceryStoryRepository<Customer>(dbContextMock.Object);
            await repository.Update(customer, default);
            dbContextMock.Verify(d => d.Update<Customer>(It.Is<Customer>(c => c.Name == customer.Name), default), Times.Once());
        }
    }
}
