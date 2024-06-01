using Moq;
using OnlineShop.ProductOptions.Dto;
using OnlineShop.ProductOptions.Repository.interfaces;
using OnlineShop.ProductOptions.Service.interfaces;
using OnlineShop.ProductOptions.Service;
using OnlineShop.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.ProductProductOptions.Service;
using Teste.ProductOptions.Helpers;
using OnlineShop.System.Constants;
using OnlineShop.ProductOptions.Model;

namespace Teste.ProductOptions.UnitTests
{
    public class TestProductOptionQueryService
    {

        private readonly Mock<IRepositoryProductOption> _mock;
        private readonly IQueryServiceProductOption _productOptionQueryService;

        public TestProductOptionQueryService()
        {
            _mock = new Mock<IRepositoryProductOption>();
            _productOptionQueryService = new QueryServiceProductOption(_mock.Object);

        }
        [Fact]
        public async Task GetAllProductOptions_ReturnAllProductOptions()
        {
            var productOptions = TestProductOptionFactory.CreateProductOptions(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(productOptions);


            var result = await _productOptionQueryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Contains(productOptions[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((ProductOption)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _productOptionQueryService.GetByIdAsync(50));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnProductOption()
        {
            var productOption = TestProductOptionFactory.CreateProductOption(12);

            _mock.Setup(repo => repo.GetByIdAsync(12)).ReturnsAsync(productOption);

            var result = await _productOptionQueryService.GetByIdAsync(12);

            Assert.NotNull(result);
            Assert.Equal(productOption, result);

        }


    }
}
