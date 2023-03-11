using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using SharedModel;

namespace RestApi_For_WebClient.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        await GetGrpcAsync();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.ToShortDateString(),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    public async Task GetGrpcAsync()
    {

        using (HttpClient client = new HttpClient())
        {
            var json = System.Text.Json.JsonSerializer.Serialize(new
            {
                name = "catcher wong"
            });


            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var resp = await client.PostAsync("http://localhost:5287/hello_world", content);

            var res = await resp.Content.ReadAsStringAsync();

            Console.WriteLine($"response result {res}");
        }
    }
}

