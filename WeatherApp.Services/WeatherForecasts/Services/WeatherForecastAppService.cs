using WeatherApp.Domain.Entities;
using WeatherApp.Services.Contract.Base.Repositories;
using WeatherApp.Services.Contract.Weathers;
using WeatherApp.Services.Contract.Weathers.Dto;

namespace WeatherApp.Services.WeatherForecasts.Services;

public class WeatherForecastAppService : IWeatherService
{
    private readonly IWeatherRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WeatherForecastAppService(
        IWeatherRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<WeatherDto> GetLatestData()
    {
        return await _repository.GetLatestData();
    }

    public async Task Create(WeatherDto dto)
    {
        var weather = new WeatherForecast
        {
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            GenerationTimeMs = dto.GenerationTimeMs,
            Elevation = dto.Elevation,
            UtcOffsetSeconds = dto.UtcOffsetSeconds,
            Timezone = dto.Timezone,
            TimezoneAbbreviation = dto.TimezoneAbbreviation,
            HourlyUnit = new HourlyUnit
            {
                Time = dto.HourlyUnits.Time,
                Temperature2M = dto.HourlyUnits.Temperature2M,
                RelativeHumidity2M = dto.HourlyUnits.RelativeHumidity2M,
                WindSpeed10M = dto.HourlyUnits.WindSpeed10M
            },
            Hourly = GenerateHourly(dto.Hourly)
        };

        await _repository.Add(weather);
        await _unitOfWork.Complete();
    }

    private HashSet<Hourly> GenerateHourly(HourlyDto hourly)
    {
        var result = new HashSet<Hourly>();
        for (var i = 0; i < hourly.Time.Length; i++)
            result.Add(new Hourly
            {
                Time = hourly.Time[i],
                Temperature2M = hourly.Temperature2M[i],
                RelativeHumidity2M = hourly.RelativeHumidity2M[i],
                WindSpeed10M = hourly.WindSpeed10m[i]
            });

        return result;
    }
}