using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Teste.Infrastructure;
using Teste.ProductOptions.Helpers;

namespace Teste.ProductOptions.UnitTests
{
    public class TestProductOptionIntegration : IClassFixture<ApiWebApplicationFactory>
    {

        private readonly HttpClient _client;

        public TestProductOptionIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllProductOptions_ProductOptionsFound_ReturnsOkStatusCode()
        {
            var createProductOptionRequest = TestProductOptionFactory.CreateProductOption(1);
            var content = new StringContent(JsonConvert.SerializeObject(createProductOptionRequest), Encoding.UTF8, "application/json");
            await _client.GetAsync("/api/v1/ControllerProductOption/all");

            var response = await _client.GetAsync("/api/v1/ControllerProductOption/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllProductOptions_ProductOptionsFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerProductOption/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProductOptionById_ProductOptionFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createProductOptionRequest = TestProductOptionFactory.CreateProductOption(1);
            var content = new StringContent(JsonConvert.SerializeObject(createProductOptionRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerProductOption/create", content);

            var response = await _client.GetAsync("api/v1/ControllerProductOption/findById/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProductOptionById_ProductOptionNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerProductOption/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
