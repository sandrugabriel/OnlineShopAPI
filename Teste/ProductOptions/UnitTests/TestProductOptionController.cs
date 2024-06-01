using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.ProductOptions.Controllers.interfaces;
using OnlineShop.ProductOptions.Controllers;
using OnlineShop.ProductOptions.Dto;
using OnlineShop.ProductOptions.Service.interfaces;
using OnlineShop.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.System.Constants;
using OnlineShop.ProductOptions.Controllers.interfaces;
using OnlineShop.ProductOptions.Controllers;
using OnlineShop.ProductOptions.Dto;
using OnlineShop.ProductOptions.Service.interfaces;
using Teste.ProductOptions.Helpers;
using OnlineShop.ProductOptions.Model;

namespace Teste.ProductOptions.UnitTests
{
    public class TestProductOptionController
    {
        private readonly Mock<IQueryServiceProductOption> _mockQueryService;
        private readonly ControlerAPIProductOption productOptionApiController;

        public TestProductOptionController()
        {
            _mockQueryService = new Mock<IQueryServiceProductOption>();

            productOptionApiController = new ControllerProductOption(_mockQueryService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));

            var restult = await productOptionApiController.GetAllAsync();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var productOptions = TestProductOptionFactory.CreateProductOptions(5);
            _mockQueryService.Setup(repo => repo.GetAllAsync()).ReturnsAsync(productOptions);

            var result = await productOptionApiController.GetAllAsync();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allProductOptions = Assert.IsType<List<ProductOption>>(okResult.Value);

            Assert.Equal(5, allProductOptions.Count);
            Assert.Equal(200, okResult.StatusCode);

        }


        [Fact]
        public async Task GetById_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetByIdAsync(10)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await productOptionApiController.GetByIdAsync(10);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var productOptions = TestProductOptionFactory.CreateProductOption(1);
            _mockQueryService.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(productOptions);

            var result = await productOptionApiController.GetByIdAsync(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allProductOptions = Assert.IsType<ProductOption>(okResult.Value);

            Assert.Equal(1, allProductOptions.Id);
            Assert.Equal(200, okResult.StatusCode);

        }
    }
}
