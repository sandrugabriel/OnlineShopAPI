using Moq;
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
    public class TestProductQueryService
    {

        private readonly Mock<IRepositoryProduct> _mock;
        private readonly IQueryServiceProduct _productQueryService;

        public TestProductQueryService()
        {
            _mock = new Mock<IRepositoryProduct>();
            _productQueryService = new QueryServiceProduct(_mock.Object);

        }
        [Fact]
        public async Task GetAllProducts_ReturnAllProducts()
        {
            var products = TestProductFactory.CreateProducts(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);


            var result = await _productQueryService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Contains(products[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((DtoProductView)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _productQueryService.GetById(50));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnProduct()
        {
            var product = TestProductFactory.CreateProduct(12);

            _mock.Setup(repo => repo.GetByIdAsync(12)).ReturnsAsync(product);

            var result = await _productQueryService.GetByIdAsync(12);

            Assert.NotNull(result);
            Assert.Equal(product, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync((DtoProductView)null);
            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _productQueryService.GetByName("test"));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByName_ReturnProduct()
        {
            var product = TestProductFactory.CreateProduct(5);
            _mock.Setup(repo => repo.GetByNameAsync("test5")).ReturnsAsync(product);
            var result = await _productQueryService.GetByNameAsync("test5");

            Assert.NotNull(result);
            Assert.Equal(product, result);

        }



    }
}
