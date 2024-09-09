using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using CustomerAPI;
using CustomerAPI.Controllers;

namespace ApiTests
{
    public class CustomersAPITests : IClassFixture<WebApplicationFactory<CustomerAPI.Program>>
    {
        private readonly WebApplicationFactory<CustomerAPI.Program> _factory;

        public CustomersAPITests(WebApplicationFactory<CustomerAPI.Program> factory)
        {
            _factory = factory;
            
        }

        [Fact]
        public async Task Get_WithValidApiKey_ReturnsSuccessAndCustomers()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-API-Key", "magic-key");

            // Act
            var response = await client.GetAsync("/customer");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var customers = JsonSerializer.Deserialize<List<Customer>>(content);
            Assert.NotNull(customers);
            Assert.NotEmpty(customers);
        }

        [Fact]
        public async Task Get_WithInvalidApiKey_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-API-Key", "invalid-magic-key");

            // Act
            var response = await client.GetAsync("/customer");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Get_WithoutApiKey_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/customer");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}