using Moq;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using OnlineShop.Options.Repository.interfaces;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Repository.interfaces;
using OnlineShop.Products.Service;
using OnlineShop.Products.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Products.Helpers;

namespace Teste.Products.UnitTests
{
    public class TestProductCommandService
    {

        private readonly Mock<IRepositoryProduct> _mock;
        private readonly Mock<IRepositoryOption> _optionMock;
        private readonly ICommandServiceProduct _commandService;

        public TestProductCommandService()
        {
            _mock = new Mock<IRepositoryProduct>();
            _optionMock = new Mock<IRepositoryOption>();
            _commandService = new CommandServiceProduct(_mock.Object,_optionMock.Object);

        }

        [Fact]
        public async Task CreateProduct_InvalidPrice()
        {
            var createRequest = new CreateRequestProduct
            {
                Category = "Test",
                Name = "Test",
                Price = 0,
                Stock = 1
            };

            _mock.Setup(repo => repo.Create(createRequest)).ReturnsAsync((DtoProductView)null);
            Exception exception = await Assert.ThrowsAsync<InvalidPrice>(() => _commandService.Create(createRequest));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task CreateProduct_ReturnProduct()
        {
            var createRequest = new CreateRequestProduct
            {
                Category = "Test",
                Name = "test",
                Price = 100,
                Stock = 1
            };

            var product = TestProductFactory.CreateProduct(50);

            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequestProduct>())).ReturnsAsync(product);

            var result = await _commandService.Create(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Name, createRequest.Name);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateRequestProduct
            {

                Category = "Test",
                Name = "Test",
                Price = 100,
                Stock = 1
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((DtoProductView)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.Update(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidPrice()
        {
            var updateRequest = new UpdateRequestProduct
            {

                Category = "Test",
                Name = "",
                Price = 0,
                Stock = 1
            };

            var product = TestProductFactory.CreateProduct(1);
            product.Price = updateRequest.Price.Value;
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _commandService.Update(1, updateRequest));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData_ReturnProduct()
        {
            var updateRequest = new UpdateRequestProduct
            {
                Category = "Test",
                Name = "Test",
                Price = 100,
                Stock = 1
            };

            var product = TestProductFactory.CreateProduct(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            _mock.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateRequestProduct>())).ReturnsAsync(product);

            var result = await _commandService.Update(1, updateRequest);

            Assert.NotNull(result);
            Assert.Equal(product, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((DtoProductView)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.Delete(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var product = TestProductFactory.CreateProduct(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var restul = await _commandService.Delete(1);

            Assert.NotNull(restul);
            Assert.Equal(product, restul);
        }

        [Fact]
        public async Task AddOption_ItemDoesNotExist()
        {
            Option option = new Option();
            option.Id = 1;
            option.Name = "test";
            _mock.Setup(repo => repo.AddOption(2,option)).ReturnsAsync((DtoProductView)null);
            Exception exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.AddOption(2, "test"));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task AddOption_ReturnProduct()
        {
            var createRequest = new CreateRequestProduct
            {
                Name = "test",
                Category = "Test",
                Price = 1234,
                Stock = 10
            };

            var product = TestProductFactory.CreateProduct(1);

            OptionResponse option = new OptionResponse();
            option.Name = "test";
            option.Price = 10;
             Option option1 = new Option();
            option1.Name = "test";
            option1.Price = 10;

            _optionMock.Setup(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(option1);
            _optionMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(option1);
            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            _mock.Setup(repo => repo.AddOption(It.IsAny<int>(), It.IsAny<Option>())).ReturnsAsync(product);
            product.Options = new List<OnlineShop.Options.Dto.OptionResponse>() { option};
            var result = await _commandService.AddOption(1, "test");

            Assert.NotNull(result);
            Assert.Equal(result.Options[0].Name, option.Name);
        }


        [Fact]
        public async Task DeleteOption_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteOption(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((DtoProductView)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.DeleteOption(1, "test"));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task DeleteOption_ValidData()
        {
            var product = TestProductFactory.CreateProduct(1);

            Option option = new Option();
            option.Id = 2;
            option.Name = "test";

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequestProduct>())).ReturnsAsync(product);
            _optionMock.Setup(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(option);


            var restul = await _commandService.DeleteOption(1,"test");

            Assert.NotNull(restul);
            Assert.Equal(product, restul);
        }


    }
}
