using WeatherApp.Services.Contract.Weathers.Dto;

namespace WeatherApp.Services.Contract.Weathers;

public interface IWeatherService
{
    public Task<WeatherDto> GetLatestData();
    public Task Create(WeatherDto dto);
}