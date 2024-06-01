using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Options.Controllers;
using OnlineShop.Options.Controllers.interfaces;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using OnlineShop.Options.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Options.Helpers;

namespace Teste.Options.UnitTests
{
    public class TestOptionController
    {

        private readonly Mock<ICommandServiceOption> _mockCommandService;
        private readonly Mock<IQueryServiceOption> _mockQueryService;
        private readonly ControllerAPIOption optionApiController;

        public TestOptionController()
        {
            _mockCommandService = new Mock<ICommandServiceOption>();
            _mockQueryService = new Mock<IQueryServiceOption>();

            optionApiController = new ControllerOption(_mockQueryService.Object, _mockCommandService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));

            var restult = await optionApiController.GetAllAsync();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var options = TestOptionFactory.CreateOptions(5);
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ReturnsAsync(options);

            var result = await optionApiController.GetAllAsync();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allOptions = Assert.IsType<List<Option>>(okResult.Value);

            Assert.Equal(5, allOptions.Count);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidName()
        {

            var createRequest = new CreateRequestOption
            {

                Name = "tesdt",
                Price = 10
            };


            _mockCommandService.Setup(repo => repo.CreateOption(It.IsAny<CreateRequestOption>())).
                ThrowsAsync(new InvalidName(Constants.InvalidName));

            var result = await optionApiController.CreateOption(createRequest);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, badRequest.StatusCode);
            Assert.Equal(Constants.InvalidName, badRequest.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreateRequestOption
            {

                Name = "tesdt",
                Price = 10
            };


            var option = TestOptionFactory.CreateOption(1);
            option.Price = createRequest.Price;
            option.Name = createRequest.Name;

            _mockCommandService.Setup(repo => repo.CreateOption(It.IsAny<CreateRequestOption>())).ReturnsAsync(option);

            var result = await optionApiController.CreateOption(createRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, option);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateRequestOption
            {
                Name = "tesdt",
                Price = 10
            };


            _mockCommandService.Setup(repo => repo.UpdateOption(1, updateRequest)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await optionApiController.UpdateOption(1, updateRequest);

            var ntFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(ntFound.StatusCode, 404);
            Assert.Equal(Constants.ItemDoesNotExist, ntFound.Value);

        }
        [Fact]
        public async Task Update_ValidData()
        {
            var updateRequest = new UpdateRequestOption
            {

                Name = "tesdt",
                Price = 10
            };



            var option = TestOptionFactory.CreateOption(1);

            _mockCommandService.Setup(repo => repo.UpdateOption(It.IsAny<int>(), It.IsAny<UpdateRequestOption>())).ReturnsAsync(option);

            var result = await optionApiController.UpdateOption(1, updateRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, option);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mockCommandService.Setup(repo => repo.DeleteOption(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await optionApiController.DeleteOption(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var option = TestOptionFactory.CreateOption(1);

            _mockCommandService.Setup(repo => repo.DeleteOption(It.IsAny<int>())).ReturnsAsync(option);

            var result = await optionApiController.DeleteOption(1);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.Equal(okresult.Value, option);

        }

    }
}
