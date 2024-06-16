using Newtonsoft.Json;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Teste.Infrastructure;
using Teste.Products.Helpers;

namespace Teste.Products
{
    public class TestProductIntegration : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TestProductIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllProducts_ProductsFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createProductRequest = TestProductFactory.CreateProduct(1);
            var content = new StringContent(JsonConvert.SerializeObject(createProductRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerProduct/createProduct", content);

            var response = await _client.GetAsync("/api/v1/ControllerProduct/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProductById_ProductFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createProductRequest = TestProductFactory.CreateProduct(2);
            var content = new StringContent(JsonConvert.SerializeObject(createProductRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Product/create", content);

            var response = await _client.GetAsync("/api/v1/ControllerProduct/findById/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProductById_ProductNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerProduct/findById/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerProduct/createProduct";
            var ControllerProduct = new CreateRequestProduct { Name = "New Product 1", Category = "asdasdon", Price = 10, Stock = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(ControllerProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Product>(responseString);

            Assert.NotNull(result);
            Assert.Equal(ControllerProduct.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerProduct/createProduct";
            var createProduct = new CreateRequestProduct { Name = "New Product 1", Category = "asdasdon", Price = 10, Stock = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Product>(responseString)!;

            request = "/api/v1/ControllerProduct/updateProduct";
            var updateProduct = new UpdateRequestProduct { Name = "New Product 3", Category = "asdasdon", Price = 10, Stock = 1 };
            content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);

            responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Product>(responseString)!;

            Assert.Equal(updateProduct.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_InvalidProductName_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerProduct/createProduct";
            var createProduct = new CreateRequestProduct { Name = "test", Category = "asdasdon", Price = 10, Stock = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DtoProductView>(responseString)!;

            request = "/api/v1/ControllerProduct/updateProduct";
            var updateProduct = new UpdateRequestProduct { Name = "",  };
            content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_Update_ProductDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerProduct/updateProduct";
            var updateProduct = new UpdateRequestProduct { Name = "New Product 3", };
            var content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_ProductExists_ReturnsDeletedProduct()
        {
            var request = "/api/v1/ControllerProduct/createProduct";
            var createProduct = new CreateRequestProduct { Name = "New Product 1", Category = "asdasdon", Price = 10, Stock = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Product>(responseString)!;

            request = $"/api/v1/ControllerProduct/deleteProduct/{result.Id}";

            response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_ProductDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerProduct/deleteProduct/77777";

            var response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
