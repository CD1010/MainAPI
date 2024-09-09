using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using OrdersAPI;

namespace ApiTests
{
    public class OrdersAPITests : IClassFixture<WebApplicationFactory<OrdersAPI.Program>>
    {
        private readonly WebApplicationFactory<OrdersAPI.Program> _factory;

        public OrdersAPITests(WebApplicationFactory<OrdersAPI.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_WithValidApiKey_ReturnsSuccessAndOrders()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-API-Key", "magic-key");

            // Act
            var response = await client.GetAsync("/orders");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<Order[]>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(orders);
            Assert.NotEmpty(orders);
            Assert.All(orders, order =>
            {
                Assert.True(order.CustomerId > 0);
                Assert.True(order.Quantity > 0);
                Assert.True(order.Date > DateOnly.MinValue);
            });
        }

        [Fact]
        public async Task Get_WithInvalidApiKey_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-API-Key", "invalid-api-key");

            // Act
            var response = await client.GetAsync("/orders");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Get_WithoutApiKey_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/orders");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

public class Order
{
    public DateOnly Date { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
}