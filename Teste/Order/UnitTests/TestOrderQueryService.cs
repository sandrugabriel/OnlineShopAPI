using Moq;
using OnlineShop.OrderDetails.Service;
using OnlineShop.Orders.Dto;
using OnlineShop.Orders.Repository.interfaces;
using OnlineShop.Orders.Service;
using OnlineShop.Orders.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Order.Helpers;

namespace Teste.Order.UnitTests
{
    public class TestOrderQueryService
    {
        private readonly Mock<IRepositoryOrder> _mock;
        private readonly IQueryServiceOrder _orderQueryService;

        public TestOrderQueryService()
        {
            _mock = new Mock<IRepositoryOrder>();
            _orderQueryService = new QueryServiceOrder(_mock.Object);

        }
        [Fact]
        public async Task GetAllOrders_ReturnAllOrders()
        {
            var orders = TestOrderFactory.CreateOrders(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);


            var result = await _orderQueryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Contains(orders[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((DtoOrderView)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _orderQueryService.GetById(50));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnOrder()
        {
            var order = TestOrderFactory.CreateOrder(12);

            _mock.Setup(repo => repo.GetByIdAsync(12)).ReturnsAsync(order);

            var result = await _orderQueryService.GetByIdAsync(12);

            Assert.NotNull(result);
            Assert.Equal(order, result);

        }


    }
}
