using Moq;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Repository.interfaces;
using OnlineShop.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.OrderDetails.Service.interfaces;
using OnlineShop.OrderDetailDetails.Service;
using Teste.OrderDetails.Helpers;
using OnlineShop.System.Constants;

namespace Teste.OrderDetails.UnitTests
{
    public class TestOrderDetailQueryService
    {
        private readonly Mock<IRepositoryOrderDetail> _mock;
        private readonly IQueryServiceOrderDetail _orderDetailQueryService;

        public TestOrderDetailQueryService()
        {
            _mock = new Mock<IRepositoryOrderDetail>();
            _orderDetailQueryService = new QueryServiceOrderDetail(_mock.Object);

        }
        [Fact]
        public async Task GetAllOrderDetails_ReturnAllOrderDetails()
        {
            var orderDetails = TestOrderDetailFactory.CreateOrderDetails(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderDetails);


            var result = await _orderDetailQueryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Contains(orderDetails[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((DtoOrderDetailView)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _orderDetailQueryService.GetById(50));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnOrderDetail()
        {
            var orderDetail = TestOrderDetailFactory.CreateOrderDetail(12);

            _mock.Setup(repo => repo.GetByIdAsync(12)).ReturnsAsync(orderDetail);

            var result = await _orderDetailQueryService.GetByIdAsync(12);

            Assert.NotNull(result);
            Assert.Equal(orderDetail, result);

        }


    }
}
