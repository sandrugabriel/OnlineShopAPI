using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Customers.Controllers;
using OnlineShop.Customers.Controllers.interfaces;
using OnlineShop.Customers.Dto;
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
    public class TestCustomerController
    {
        private readonly Mock<ICommandServiceCustomer> _mockCommandService;
        private readonly Mock<IQueryServiceCustomer> _mockQueryService;
        private readonly ControllerAPICustomer customerApiController;

        public TestCustomerController()
        {
            _mockCommandService = new Mock<ICommandServiceCustomer>();
            _mockQueryService = new Mock<IQueryServiceCustomer>();

            customerApiController = new ControllerCustomer(_mockQueryService.Object, _mockCommandService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));

            var restult = await customerApiController.GetAllAsync();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var customers = TestCustomerFactory.CreateCustomers(5);
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            var result = await customerApiController.GetAllAsync();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allCustomers = Assert.IsType<List<DtoCustomerView>>(okResult.Value);

            Assert.Equal(5, allCustomers.Count);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidName()
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


            _mockCommandService.Setup(repo => repo.CreateCustomer(It.IsAny<CreateRequestCustomer>())).
                ThrowsAsync(new InvalidName(Constants.InvalidName));

            var result = await customerApiController.CreateCustomer(createRequest);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, badRequest.StatusCode);
            Assert.Equal(Constants.InvalidName, badRequest.Value);

        }

        [Fact]
        public async Task Create_ValidData()
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

            var customer = TestCustomerFactory.CreateCustomer(1);
            customer.FullName = createRequest.FullName;
            customer.Email = createRequest.Email;

            _mockCommandService.Setup(repo => repo.CreateCustomer(It.IsAny<CreateRequestCustomer>())).ReturnsAsync(customer);

            var result = await customerApiController.CreateCustomer(createRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, customer);

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


            _mockCommandService.Setup(repo => repo.UpdateCustomer(1, updateRequest)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await customerApiController.UpdateCustomer(1, updateRequest);

            var ntFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(ntFound.StatusCode, 404);
            Assert.Equal(Constants.ItemDoesNotExist, ntFound.Value);

        }
        [Fact]
        public async Task Update_ValidData()
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

            _mockCommandService.Setup(repo => repo.UpdateCustomer(It.IsAny<int>(), It.IsAny<UpdateRequestCustomer>())).ReturnsAsync(customer);

            var result = await customerApiController.UpdateCustomer(1, updateRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, customer);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mockCommandService.Setup(repo => repo.DeleteCustomer(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await customerApiController.DeleteCustomer(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var customer = TestCustomerFactory.CreateCustomer(1);

            _mockCommandService.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).ReturnsAsync(customer);

            var result = await customerApiController.DeleteCustomer(1);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.Equal(okresult.Value, customer);

        }


    }
}
