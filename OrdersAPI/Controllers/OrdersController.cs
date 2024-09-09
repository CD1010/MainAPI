using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrderAPI;

namespace OrdersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
  

    private readonly ILogger<OrdersController> _logger;
    private readonly IConfiguration _configuration;

    public OrdersController(ILogger<OrdersController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet(Name = "GetOrders")]
    public  async Task<IActionResult> Get()
    {
        var apiKey = _configuration["ApiKey"];
        var providedApiKey = Request.Headers["X-API-Key"].FirstOrDefault();

        if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != apiKey)
        {
            return Unauthorized("Invalid API Key");
        }

        Random r = new Random();
        var forecast = Enumerable.Range(1, 5).Select(index => new Order
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            CustomerId = index,
            Quantity = r.Next(1, 5)
        })
        .ToArray();

        return Ok(forecast);
    }
}
