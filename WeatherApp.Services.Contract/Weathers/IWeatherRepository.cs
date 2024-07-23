using WeatherApp.Domain.Entities;
using WeatherApp.Services.Contract.Weathers.Dto;

namespace WeatherApp.Services.Contract.Weathers;

public interface IWeatherRepository
{
    public Task Add(WeatherForecast weatherForecast);
    public Task<WeatherDto> GetLatestData();
}