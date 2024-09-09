using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Shared.Models;
using Shared;

namespace MainAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase
{
    private readonly ILogger<MainController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAuthClientHelper _authClientHelper;

    public MainController(ILogger<MainController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory, IAuthClientHelper authClientHelper)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _authClientHelper = authClientHelper;
    }

    [HttpGet(Name = "GetCustomersAndOrders")]
    public async Task<IActionResult> Get()
    {
        return await FetchCustomersAndOrders();
    }

    [HttpPost("MainPost")]
    public async Task<IActionResult> MainPost()
    {
        return await FetchCustomersAndOrders();
    }

    [HttpGet("customers")]
    public async Task<ActionResult<List<Customer>>> GetCustomers()
    {
        var customers = await FetchCustomers();
        if (customers == null) return Unauthorized("Invalid API Key");
        return Ok(customers);
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await FetchOrders();
        if (orders == null) return Unauthorized("Invalid API Key");
        return Ok(orders);
    }

    private async Task<IActionResult> FetchCustomersAndOrders()
    {
        var customers = await FetchCustomers();
        var orders = await FetchOrders();

        if (customers == null || orders == null)
        {
            return Unauthorized("Invalid API Key");
        }

        var result = new CustomersOrdersResponse()
        {
            Customers = customers,
            Orders = orders
        };

        return Ok(result);
    }

    private async Task<List<Customer>> FetchCustomers()
    {
        var key = _configuration["ApiKey"];

        var httpClient = _authClientHelper.GetAuthorizedHttpClient(key);

        if (httpClient == null) return null;

        var response = await httpClient.GetFromJsonAsync<List<Customer>>("https://localhost:7128/customer");
        return response;
    }

    private async Task<List<Order>> FetchOrders()
    {
        var key = _configuration["ApiKey"];

        var httpClient = _authClientHelper.GetAuthorizedHttpClient(key);
        if (httpClient == null) return null;

        var response = await httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7193/orders");
        return response;
    }

    
}
