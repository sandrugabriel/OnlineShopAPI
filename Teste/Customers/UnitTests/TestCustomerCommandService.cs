using AutoMapper;
using Moq;
using OnlineShop.Customers.Dto;
using OnlineShop.Customers.Models;
using OnlineShop.Customers.Repository.interfaces;
using OnlineShop.Customers.Services;
using OnlineShop.Customers.Services.interfaces;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;
using OnlineShop.OrderDetails.Repository.interfaces;
using OnlineShop.Orders.Dto;
using OnlineShop.Orders.Repository.interfaces;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using OnlineShop.Products.Repository.interfaces;
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
    public class TestCustomerCommandService
    {

        private readonly Mock<IRepositoryCustomer> _mock;
        private readonly Mock<IRepositoryProduct> _productMock;
        private readonly Mock<IRepositoryOrder> _orderMock;
        private readonly Mock<IRepositoryOrderDetail> _orderDetailMock;
        private readonly ICommandServiceCustomer _commandService;

        public TestCustomerCommandService()
        {
            _mock = new Mock<IRepositoryCustomer>();
            _orderMock = new Mock<IRepositoryOrder>();
            _productMock = new Mock<IRepositoryProduct>();
            _orderDetailMock = new Mock<IRepositoryOrderDetail>();
            _commandService = new CommandServiceCustomer(_mock.Object,_orderMock.Object,_productMock.Object, _orderDetailMock.Object);
       
        }

        [Fact]
        public async Task CreateCustomer_InvalidName()
        {
            var createRequest = new CreateRequestCustomer
            {
                Address = "tesdt",
                Country = "test",
                FullName = "",
                Password = "1234",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            _mock.Setup(repo => repo.CreateCustomer(createRequest)).ReturnsAsync((DtoCustomerView)null);
            Exception exception = await Assert.ThrowsAsync<InvalidName>(() => _commandService.CreateCustomer(createRequest));

            Assert.Equal(Constants.InvalidName, exception.Message);
        }

        [Fact]
        public async Task CreateCustomer_ReturnCustomer()
        {
            var createRequest = new CreateRequestCustomer
            {

                Address = "tesdt",
                Country = "test",
                FullName = "test50",
                Password = "1234",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(50);

            _mock.Setup(repo => repo.CreateCustomer(It.IsAny<CreateRequestCustomer>())).ReturnsAsync(customer);

            var result = await _commandService.CreateCustomer(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.FullName, createRequest.FullName);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateRequestCustomer
            {

                Address = "tesdt",
                Country = "test",
                FullName = "Test",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((DtoCustomerView)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.UpdateCustomer(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidName()
        {
            var updateRequest = new UpdateRequestCustomer
            {

                Address = "tesdt",
                Country = "test",
                FullName = "",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(1);
            customer.FullName = updateRequest.FullName;
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            var exception = await Assert.ThrowsAsync<InvalidName>(() => _commandService.UpdateCustomer(1, updateRequest));

            Assert.Equal(Constants.InvalidName, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData_ReturnCustomer()
        {
            var updateRequest = new UpdateRequestCustomer
            {

                Address = "tesdt",
                Country = "test",
                FullName = "Test",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mock.Setup(repo => repo.UpdateCustomer(It.IsAny<int>(), It.IsAny<UpdateRequestCustomer>())).ReturnsAsync(customer);

            var result = await _commandService.UpdateCustomer(1, updateRequest);

            Assert.NotNull(result);
            Assert.Equal(customer, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).ReturnsAsync((DtoCustomerView)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.DeleteCustomer(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var customer = TestCustomerFactory.CreateCustomer(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var restul = await _commandService.DeleteCustomer(1);

            Assert.NotNull(restul);
            Assert.Equal(customer, restul);
        }

        [Fact]
        public async Task AddProductToOrder_ItemDoesNotExist()
        {
            var createRequest = new CreateRequestProduct
            {
                Name = "test",
                Category = "Test",
                Price = 1234,
                Stock = 10
            };

            _mock.Setup(repo => repo.AddProductToOrder(2, "test","test",1)).ReturnsAsync((DtoCustomerView)null);
            Exception exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.AddProductToOrder(2, "test", "test", 1));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task AddProductToOrder_ReturnCustomer()
        {
            var createRequest = new CreateRequestProduct
            {
                Name = "test",
                Category = "Test",
                Price = 1234,
                Stock = 10
            };

            var customer = TestCustomerFactory.CreateCustomer(1);

            DtoProductViewForOrder product = new DtoProductViewForOrder();
            product.Name = createRequest.Name;
            DtoOrderView view = new DtoOrderView();
            DtoOrderDetailView dtoOrderDetailView = new DtoOrderDetailView();
            dtoOrderDetailView.Product = product;
            view.Products = new List<OnlineShop.OrderDetails.Dto.DtoOrderDetailView> {dtoOrderDetailView};
            customer.Orders = new List<OnlineShop.Orders.Dto.DtoOrderView>() { view};

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mock.Setup(repo => repo.AddProductToOrder(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(customer);

            var result = await _commandService.AddProductToOrder(1, "test", "test", 1);

            Assert.NotNull(result);
            Assert.Equal(result.Orders[0].Products[0].Product.Name, createRequest.Name);
        }


        [Fact]
        public async Task DeleteOrder_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteOrder(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((DtoCustomerView)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.DeleteOrder(1, 2));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task DeleteOrder_ValidData()
        {
            var customer = TestCustomerFactory.CreateCustomer(1);

            DtoOrderView dto = new DtoOrderView();
            dto.Id = 2;
            customer.Orders = new List<DtoOrderView>() { dto };
             
            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mock.Setup(repo => repo.CreateCustomer(It.IsAny<CreateRequestCustomer>())).ReturnsAsync(customer);
            _orderMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(dto);


            var restul = await _commandService.DeleteOrder(1, 2);

            Assert.NotNull(restul);
            Assert.Equal(customer, restul);
        }

        

    }
}
