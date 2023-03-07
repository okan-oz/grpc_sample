using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using WebClients.Models;
using Microsoft.AspNetCore;
using System.Security.Cryptography.Xml;
using SharedModel;

namespace WebClients.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //comment
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetForecast()
    {

        string url = "http://localhost:5279/WeatherForecast";

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(url);

        // Add an Accept header for JSON format.
        client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

        IEnumerable<WeatherForecast> WeatherForecastObjects = new List<WeatherForecast> {};
        // List data response.
        HttpResponseMessage response =  client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        if (response.IsSuccessStatusCode)
        {
            // Parse the response body.
             WeatherForecastObjects = response.Content.ReadAsAsync<IEnumerable<WeatherForecast>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            
        }
        else
        {
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        }



        return View(WeatherForecastObjects);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

