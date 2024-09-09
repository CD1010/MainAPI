using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace MainAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase
{
    private readonly ILogger<MainController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public MainController(ILogger<MainController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet(Name = "GetCustomersAndOrders")]
    public async Task<IActionResult> Get()
    {
        var apiKey = _configuration["ApiKey"];
        var providedApiKey = Request.Headers["X-API-Key"].FirstOrDefault();

        if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != apiKey)
        {
            return Unauthorized("Invalid API Key");
        }

        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);

        var customersTask = httpClient.GetStringAsync("https://localhost:7128/customer");
        var ordersTask = httpClient.GetStringAsync("https://localhost:7193/orders");

        await Task.WhenAll(customersTask, ordersTask);

        var customers = JsonSerializer.Deserialize<object>(await customersTask);
        var orders = JsonSerializer.Deserialize<object>(await ordersTask);

        var result = new
        {
            Customers = customers,
            Orders = orders
        };

        return Ok(result);
    }
}
