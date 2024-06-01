using Moq;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using OnlineShop.Options.Repository.interfaces;
using OnlineShop.Options.Service;
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
    public class TestOptionCommandService
    {
        private readonly Mock<IRepositoryOption> _mock;
        private readonly ICommandServiceOption _commandService;

        public TestOptionCommandService()
        {
            _mock = new Mock<IRepositoryOption>();
            _commandService = new CommandServiceOption(_mock.Object);

        }

        [Fact]
        public async Task CreateOption_InvalidName()
        {
            var createRequest = new CreateRequestOption
            {
                Name = "",
                Price = 100
            };

            _mock.Setup(repo => repo.CreateOption(createRequest)).ReturnsAsync((Option)null);
            Exception exception = await Assert.ThrowsAsync<InvalidName>(() => _commandService.CreateOption(createRequest));

            Assert.Equal(Constants.InvalidName, exception.Message);
        }

        [Fact]
        public async Task CreateOption_ReturnOption()
        {
            var createRequest = new CreateRequestOption
            {
                Name = "test50",
                Price = 100
            };


            var option = TestOptionFactory.CreateOption(50);

            _mock.Setup(repo => repo.CreateOption(It.IsAny<CreateRequestOption>())).ReturnsAsync(option);

            var result = await _commandService.CreateOption(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Name, createRequest.Name);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateRequestOption
            {
                Name = "test",
                Price = 100
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((Option)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.UpdateOption(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidName()
        {
            var updateRequest = new UpdateRequestOption
            {
                Name = "test",
                Price = 100
            };

            var option = TestOptionFactory.CreateOption(1);
            option.Name = updateRequest.Name;
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(option);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.UpdateOption(1, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData_ReturnOption()
        {
            var updateRequest = new UpdateRequestOption
            {
                Name = "test",
                Price = 100
            };

            var option = TestOptionFactory.CreateOption(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(option);
            _mock.Setup(repo => repo.UpdateOption(It.IsAny<int>(), It.IsAny<UpdateRequestOption>())).ReturnsAsync(option);

            var result = await _commandService.UpdateOption(1, updateRequest);

            Assert.NotNull(result);
            Assert.Equal(option, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteOption(It.IsAny<int>())).ReturnsAsync((Option)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.DeleteOption(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var option = TestOptionFactory.CreateOption(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(option);

            var restul = await _commandService.DeleteOption(1);

            Assert.NotNull(restul);
            Assert.Equal(option, restul);
        }

    }
}
