using Moq;
using OnlineShop.Customers.Dto;
using OnlineShop.Customers.Models;
using OnlineShop.Customers.Repository.interfaces;
using OnlineShop.Customers.Services;
using OnlineShop.Customers.Services.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.DtoCustomerViews.Helpers;

namespace Teste.Customers.UnitTests
{
    public class TestCustomerQueryService
    {
        private readonly Mock<IRepositoryCustomer> _mock;
        private readonly IQueryServiceCustomer _customerQueryService;

        public TestCustomerQueryService()
        {
            _mock = new Mock<IRepositoryCustomer>();
            _customerQueryService = new QueryServiceCustomer(_mock.Object);

        }
        [Fact]
        public async Task GetAllCustomers_ReturnAllCustomers()
        {
            var customers = TestCustomerFactory.CreateCustomers(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);


            var result = await _customerQueryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Contains(customers[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((DtoCustomerView)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _customerQueryService.GetById(50));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnCustomer()
        {
            var customer = TestCustomerFactory.CreateCustomer(12);

            _mock.Setup(repo => repo.GetByIdAsync(12)).ReturnsAsync(customer);

            var result = await _customerQueryService.GetById(12);

            Assert.NotNull(result);
            Assert.Equal(customer, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync((DtoCustomerView)null);
            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _customerQueryService.GetByName("test"));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByName_ReturnCustomer()
        {
            var customer = TestCustomerFactory.CreateCustomer(5);
            _mock.Setup(repo => repo.GetByNameAsync("test5")).ReturnsAsync(customer);
            var result = await _customerQueryService.GetByName("test5");

            Assert.NotNull(result);
            Assert.Equal(customer, result);

        }


    }
}
