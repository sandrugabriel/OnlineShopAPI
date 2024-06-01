using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Orders.Controllers;
using OnlineShop.Orders.Controllers.interfaces;
using OnlineShop.Orders.Dto;
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
    public class TestOrderController
    {

        private readonly Mock<IQueryServiceOrder> _mockQueryService;
        private readonly ControllerAPIOrder orderApiController;

        public TestOrderController()
        {
            _mockQueryService = new Mock<IQueryServiceOrder>();

            orderApiController = new ControllerOrder(_mockQueryService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));

            var restult = await orderApiController.GetOrders();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var orders = TestOrderFactory.CreateOrders(5);
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);

            var result = await orderApiController.GetOrders();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allOrders = Assert.IsType<List<DtoOrderView>>(okResult.Value);

            Assert.Equal(5, allOrders.Count);
            Assert.Equal(200, okResult.StatusCode);

        }


        [Fact]
        public async Task GetById_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetByIdAsync(10)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await orderApiController.GetById(10);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var orders = TestOrderFactory.CreateOrder(1);
            _mockQueryService.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(orders);

            var result = await orderApiController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allOrders = Assert.IsType<DtoOrderView>(okResult.Value);

            Assert.Equal(1, allOrders.Id);
            Assert.Equal(200, okResult.StatusCode);

        }
    }
}
