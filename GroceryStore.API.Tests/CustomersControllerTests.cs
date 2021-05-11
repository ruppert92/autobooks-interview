using AutoMapper;
using GroceryStore.API.Controllers;
using GroceryStore.API.Models;
using GroceryStore.BLL.DTOs;
using GroceryStore.BLL.Interfaces;
using GroceryStore.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GroceryStore.API.Tests
{
    public class CustomersControllerTests
    {
        private readonly IMapper _mapper;

        public CustomersControllerTests()
        {
            _mapper = new Mapper(AutoMapperHelper.ConfigureAutomapper());
        }

        [Fact]
        public async Task Should_Return_Customer_When_Found()
        {
            var customerServiceMock = new Mock<ICustomersService>();
            var customer = new CustomerDTO() { Id = 1, Name = "Test" };
            customerServiceMock.Setup(s => s.GetById(1, default)).Returns(Task.FromResult(customer));

            var controller = new CustomersController(customerServiceMock.Object, _mapper);
            var result = await controller.Get(1, default);
            Assert.Equal(customer, ((OkObjectResult)result).Value);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task Should_Return_404_When_Customer_Not_Found()
        {
            var customerServiceMock = new Mock<ICustomersService>();

            var controller = new CustomersController(customerServiceMock.Object, _mapper);
            var result = await controller.Get(1, default);
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        [Fact]
        public async Task Should_Return_Customers()
        {
            var customerServiceMock = new Mock<ICustomersService>();
            var customers = new List<CustomerDTO>() { new CustomerDTO() { Id = 1, Name = "Test" }, new CustomerDTO() { Id = 2, Name = "Test2" } };
            customerServiceMock.Setup(s => s.GetAll(default)).Returns(Task.FromResult((IEnumerable<CustomerDTO>)customers));

            var controller = new CustomersController(customerServiceMock.Object, _mapper);
            var result = await controller.Get(default);
            Assert.Equal(customers, ((OkObjectResult)result).Value);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task Should_Return_New_Customer_When_Added()
        {
            var customerServiceMock = new Mock<ICustomersService>();
            var postRequest = new AddCustomerRequest() { Name = "Test" };
            var customer = new CustomerDTO() { Id = 1, Name = "Test" };
            customerServiceMock.Setup(s => s.Add(It.Is<CustomerDTO>(c => c.Name == customer.Name), default)).Returns(Task.FromResult(customer));

            var controller = new CustomersController(customerServiceMock.Object, _mapper);
            var result = await controller.Post(postRequest, default);
            Assert.Equal(customer, ((CreatedAtActionResult)result).Value);
            Assert.Equal(201, ((CreatedAtActionResult)result).StatusCode);
        }

        [Fact]
        public async Task Should_Return_204_When_Updated()
        {
            var customerServiceMock = new Mock<ICustomersService>();
            var customer = new CustomerDTO() { Id = 1, Name = "Test2" };
            customerServiceMock.Setup(s => s.Update(customer, default)).Returns(Task.CompletedTask);

            var controller = new CustomersController(customerServiceMock.Object, _mapper);
            var result = await controller.Put(1, customer, default);
            Assert.Equal(204, ((NoContentResult)result).StatusCode);
        }
    }
}
