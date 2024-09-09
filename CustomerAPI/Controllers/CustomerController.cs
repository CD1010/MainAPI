using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Text.Json;

namespace CustomerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{



    private readonly IConfiguration _configuration;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }


    [HttpGet(Name = "customer")]
    public async Task<ActionResult<List<Customer>>> Get()
    {
        var apiKey = _configuration["ApiKey"];
        var providedApiKey = Request.Headers["X-API-Key"].FirstOrDefault();

        if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != apiKey)
        {
            return Unauthorized("Invalid API Key");
        }
        await Task.Delay(500);
        var ret =  await Task.FromResult(Enumerable.Range(1, 5).Select(index => new Customer
        {
            Email = $"{index}@nowhere.com",
            Name = "User " + index.ToString(),
            Id = index
        })
        .ToList<Customer>());

        return Ok(ret);
        

    }
}
