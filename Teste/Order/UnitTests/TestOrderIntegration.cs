using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Teste.Infrastructure;
using Teste.Order.Helpers;

namespace Teste.Order.UnitTests
{
    public class TestOrderIntegration : IClassFixture<ApiWebApplicationFactory>
    {


        private readonly HttpClient _client;

        public TestOrderIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllOrders_OrdersFound_ReturnsOkStatusCode()
        {
            var createOrderRequest = TestOrderFactory.CreateOrder(1);
            var content = new StringContent(JsonConvert.SerializeObject(createOrderRequest), Encoding.UTF8, "application/json");
            await _client.GetAsync("/api/v1/ControllerOrder/all");

            var response = await _client.GetAsync("/api/v1/ControllerOrder/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllOrders_OrdersFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerOrder/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOrderById_OrderFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createOrderRequest = TestOrderFactory.CreateOrder(1);
            var content = new StringContent(JsonConvert.SerializeObject(createOrderRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerOrder/create", content);

            var response = await _client.GetAsync("api/v1/ControllerOrder/findById/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOrderById_OrderNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerOrder/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
