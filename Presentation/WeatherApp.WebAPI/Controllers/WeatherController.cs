using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services.Contract.Weathers.Dto;
using WeatherApp.Services.WeatherForecasts.Handlers;

namespace WeatherApp.WebAPI.Controllers;

[ApiController]
[Route("weather-forecasts")]
public class WeatherController : ControllerBase
{
    private readonly WeatherForecastDataHandler _handler;

    public WeatherController(WeatherForecastDataHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public async Task<WeatherDto> GetWeatherForecastData()
    {
        return await _handler.HandleAsync();
    }
}