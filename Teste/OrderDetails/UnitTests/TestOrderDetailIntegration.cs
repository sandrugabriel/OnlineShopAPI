using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Teste.Infrastructure;
using Teste.OrderDetails.Helpers;

namespace Teste.OrderDetails.UnitTests
{
    public class TestOrderDetailIntegration : IClassFixture<ApiWebApplicationFactory>
    {


        private readonly HttpClient _client;

        public TestOrderDetailIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllOrderDetails_OrderDetailsFound_ReturnsOkStatusCode()
        {
            var createOrderDetailRequest = TestOrderDetailFactory.CreateOrderDetail(1);
            var content = new StringContent(JsonConvert.SerializeObject(createOrderDetailRequest), Encoding.UTF8, "application/json");
            await _client.GetAsync("/api/v1/ControllerOrderDetail/all");

            var response = await _client.GetAsync("/api/v1/ControllerOrderDetail/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllOrderDetails_OrderDetailsFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerOrderDetail/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOrderDetailById_OrderDetailFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createOrderDetailRequest = TestOrderDetailFactory.CreateOrderDetail(1);
            var content = new StringContent(JsonConvert.SerializeObject(createOrderDetailRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerOrderDetail/create", content);

            var response = await _client.GetAsync("api/v1/ControllerOrderDetail/findById/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOrderDetailById_OrderDetailNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerOrderDetail/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
