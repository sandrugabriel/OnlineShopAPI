using Moq;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Repository.interfaces;
using OnlineShop.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Options.Service.interfaces;
using OnlineShop.Options.Service;
using Teste.Options.Helpers;
using OnlineShop.Options.Models;
using OnlineShop.System.Constants;

namespace Teste.Options.UnitTests
{
    public class TestOptionQueryService
    {
        private readonly Mock<IRepositoryOption> _mock;
        private readonly IQueryServiceOption _optionQueryService;

        public TestOptionQueryService()
        {
            _mock = new Mock<IRepositoryOption>();
            _optionQueryService = new QueryServiceOption(_mock.Object);

        }
        [Fact]
        public async Task GetAllOptions_ReturnAllOptions()
        {
            var options = TestOptionFactory.CreateOptions(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(options);


            var result = await _optionQueryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Contains(options[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((Option)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _optionQueryService.GetByIdAsync(50));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnOption()
        {
            var option = TestOptionFactory.CreateOption(12);

            _mock.Setup(repo => repo.GetByIdAsync(12)).ReturnsAsync(option);

            var result = await _optionQueryService.GetByIdAsync(12);

            Assert.NotNull(result);
            Assert.Equal(option, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync((Option)null);
            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _optionQueryService.GetByNameAsync("test"));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByName_ReturnOption()
        {
            var option = TestOptionFactory.CreateOption(5);
            _mock.Setup(repo => repo.GetByNameAsync("test5")).ReturnsAsync(option);
            var result = await _optionQueryService.GetByNameAsync("test5");

            Assert.NotNull(result);
            Assert.Equal(option, result);

        }


    }
}
