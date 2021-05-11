using AutoMapper;
using GroceryStore.BLL.DTOs;
using GroceryStore.BLL.Services;
using GroceryStore.Common.Helpers;
using GroceryStore.DAL.Interfaces;
using GroceryStore.DAL.Models;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GroceryStore.BLL.Tests
{
    public class CustomersServiceTests
    {
        private readonly IMapper _mapper;

        public CustomersServiceTests()
        {
            _mapper = new Mapper(AutoMapperHelper.ConfigureAutomapper());
        }

        [Fact]
        public async Task Should_Return_CustomerDto_By_Id()
        {
            var customerRepositoryMock = new Mock<IGroceryStoreRepository<Customer>>();
            var customer = new Customer() { Id = 1, Name = "Test" };
            customerRepositoryMock.Setup(r => r.GetById(1, default)).Returns(Task.FromResult(customer));

            var service = new CustomersService(customerRepositoryMock.Object, _mapper);
            var result = await service.GetById(1, default);
            // Use JsonConvert to avoid custom equality checker. Otherwise it compares reference and they are not the same
            Assert.Equal(JsonConvert.SerializeObject(_mapper.Map<CustomerDTO>(customer)), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async Task Should_Return_All_CustomerDtos()
        {
            var customerRepositoryMock = new Mock<IGroceryStoreRepository<Customer>>();
            var customers = new List<Customer>() { new Customer() { Id = 1, Name = "Test" }, new Customer() { Id = 2, Name = "Test2" } };
            customerRepositoryMock.Setup(r => r.GetAll(default)).Returns(Task.FromResult((IEnumerable<Customer>)customers));

            var service = new CustomersService(customerRepositoryMock.Object, _mapper);
            var result = await service.GetAll(default);
            // Use JsonConvert to avoid custom equality checker. Otherwise it compares reference and they are not the same
            Assert.Equal(JsonConvert.SerializeObject(_mapper.Map<List<CustomerDTO>>(customers)), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async Task Should_Return_New_CustomerDto()
        {
            var customerRepositoryMock = new Mock<IGroceryStoreRepository<Customer>>();
            var customerWithoutId = new Customer() { Name = "Test" };
            var customerWithId = new Customer() { Id = 12, Name = "Test" };
            var customerDtoWithoutId = _mapper.Map<CustomerDTO>(customerWithoutId);
            var customerDtoWithId = _mapper.Map<CustomerDTO>(customerWithId);
            customerRepositoryMock.Setup(r => r.Add(It.Is<Customer>(c => c.Name == customerWithoutId.Name), default)).Returns(Task.FromResult(customerWithId));

            var service = new CustomersService(customerRepositoryMock.Object, _mapper);
            var result = await service.Add(customerDtoWithoutId, default);
            // Use JsonConvert to avoid custom equality checker. Otherwise it compares reference and they are not the same
            Assert.Equal(JsonConvert.SerializeObject(customerDtoWithId), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async Task Should_Update_Customer()
        {
            var customerRepositoryMock = new Mock<IGroceryStoreRepository<Customer>>();
            var customer = new Customer() { Id = 3, Name = "Test2" };
            customerRepositoryMock.Setup(r => r.Update(It.Is<Customer>(c => c.Id == customer.Id), default)).Returns(Task.CompletedTask);

            var service = new CustomersService(customerRepositoryMock.Object, _mapper);
            var customerDto = _mapper.Map<CustomerDTO>(customer);
            await service.Update(customerDto, default);
            customerRepositoryMock.Verify(r => r.Update(It.Is<Customer>(c => c.Id == customer.Id), default), Times.Once());
        }
    }
}
