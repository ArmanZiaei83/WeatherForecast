using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.Entities;
using WeatherApp.Persistence.Context;
using WeatherApp.Services.Contract.Weathers;
using WeatherApp.Services.Contract.Weathers.Dto;

namespace WeatherApp.Persistence.Weathers;

public class WeatherRepository : IWeatherRepository
{
    private readonly DbSet<WeatherForecast> _weathers;

    public WeatherRepository(DataContext context)
    {
        _weathers = context.Set<WeatherForecast>();
    }

    public async Task Add(WeatherForecast weatherForecast)
    {
        await _weathers.AddAsync(weatherForecast);
    }

    public async Task<WeatherDto> GetLatestData()
    {
        return await _weathers
            .OrderByDescending(_ => _.Id)
            .Select(_ => new WeatherDto
            {
                Latitude = _.Latitude,
                Longitude = _.Longitude,
                GenerationTimeMs = _.GenerationTimeMs,
                Elevation = _.Elevation,
                UtcOffsetSeconds = _.UtcOffsetSeconds,
                Timezone = _.Timezone,
                TimezoneAbbreviation = _.TimezoneAbbreviation,
                HourlyUnits = new HourlyUnitDto
                {
                    Time = _.HourlyUnit.Time,
                    Temperature2M = _.HourlyUnit.Temperature2M,
                    RelativeHumidity2M = _.HourlyUnit.RelativeHumidity2M,
                    WindSpeed10M = _.HourlyUnit.WindSpeed10M
                },
                Hourly = new HourlyDto
                {
                    Time = _.Hourly.Select(h => h.Time).ToArray(),
                    Temperature2M =
                        _.Hourly.Select(h => h.Temperature2M).ToArray(),
                    RelativeHumidity2M = _.Hourly
                        .Select(r => r.RelativeHumidity2M).ToArray(),
                    WindSpeed10m =
                        _.Hourly.Select(r => r.WindSpeed10M).ToArray()
                }
            })
            .AsSplitQuery()
            .FirstOrDefaultAsync();
    }
}