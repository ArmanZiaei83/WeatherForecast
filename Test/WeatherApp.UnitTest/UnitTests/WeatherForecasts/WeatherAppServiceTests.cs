using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.Entities;
using WeatherApp.Persistence.Context;
using WeatherApp.Services.Contract.Weathers;
using WeatherApp.UnitTest.Infrastructure;
using WeatherApp.UnitTest.TestTools.Builder;
using WeatherApp.UnitTest.TestTools.Factory;
using Xunit;

namespace WeatherApp.UnitTest.UnitTests.WeatherForecasts;

public class WeatherAppServiceTests
{
    private readonly DataContext _dbContext;
    private readonly IWeatherService _sut;

    public WeatherAppServiceTests()
    {
        _dbContext = new EfInMemoryDatabase().CreateDataContext<DataContext>();
        _sut = WeatherServiceFactory.GenerateService(_dbContext);
    }

    [Fact]
    public async Task Create_CreatesWeather_Properly()
    {
        var testDate = new DateTime(1, 1, 1);
        var dto = new WeatherDtoBuilder()
            .WithLatitude(20.0F)
            .WithLongitude(21.0F)
            .WithGenerationTimeMs(22.0F)
            .WithElevation(23.0F)
            .WithUtcOffsetSeconds(10)
            .WithTimezone("dummy-timezone")
            .WithTimezoneAbbreviation("dummy-timezone-abbrev")
            .WithHourlyUnits(new HourlyUnitDtoBuilder()
                .WithTime("dummy-time")
                .WithRelativeHumidity2M("dummy-relative")
                .WithWindSpeed10M("dummy-wind-speed")
                .WithTemperature2M("dummy-temp")
                .Build())
            .WithHourly(new HourlyDtoBuilder()
                .WithTime(new[] { testDate })
                .WithTemperature2M(new[] { 1.1F })
                .WithRelativeHumidity2M(new[] { 1.1F })
                .WithWindSpeed10M(new[] { 1.1F }).Build())
            .Build();

        await _sut.Create(dto);

        var actualWeather = await _dbContext.WeatherForecasts
            .Include(_ => _.Hourly)
            .Include(_ => _.HourlyUnit)
            .FirstAsync();
        actualWeather.Elevation.Should().Be(dto.Elevation);
        actualWeather.Latitude.Should().Be(dto.Latitude);
        actualWeather.Longitude.Should().Be(dto.Longitude);
        actualWeather.Timezone.Should().Be(dto.Timezone);
        actualWeather.TimezoneAbbreviation.Should()
            .Be(dto.TimezoneAbbreviation);
        actualWeather.GenerationTimeMs.Should().Be(dto.GenerationTimeMs);
        actualWeather.UtcOffsetSeconds.Should().Be(dto.UtcOffsetSeconds);
        actualWeather.HourlyUnit.Temperature2M.Should()
            .Be(dto.HourlyUnits.Temperature2M);
        actualWeather.HourlyUnit.Time.Should().Be(dto.HourlyUnits.Time);
        actualWeather.HourlyUnit.RelativeHumidity2M.Should()
            .Be(dto.HourlyUnits.RelativeHumidity2M);
        actualWeather.HourlyUnit.WindSpeed10M.Should()
            .Be(dto.HourlyUnits.WindSpeed10M);
        actualWeather.Hourly.Should().ContainSingle(
            _ => _.WeatherForecastId.Equals(actualWeather.Id) &&
                 _.Time.Equals(testDate) &&
                 _.Temperature2M.Equals(dto.Hourly.Temperature2M[0]) &&
                 _.RelativeHumidity2M.Equals(
                     dto.Hourly.RelativeHumidity2M[0]) &&
                 _.WindSpeed10M.Equals(dto.Hourly.WindSpeed10m[0]));
    }

    [Fact]
    public async Task GetLatestData_QueriesLastPersistedWeather_Properly()
    {
        const float expectedLatitude = 3.2F;
        const float expectedLongitude = 4.1F;
        var firstWeather = CreateWeather(1, 2);
        var secondWeather = CreateWeather(expectedLatitude, expectedLongitude);

        var actualResult = await _sut.GetLatestData();

        actualResult.Latitude.Should().Be(expectedLatitude);
        actualResult.Longitude.Should().Be(expectedLongitude);
        actualResult.Elevation.Should().Be(secondWeather.Elevation);
        actualResult.GenerationTimeMs.Should()
            .Be(secondWeather.GenerationTimeMs);
        actualResult.Timezone.Should().Be(secondWeather.Timezone);
        actualResult.TimezoneAbbreviation.Should()
            .Be(secondWeather.TimezoneAbbreviation);
        actualResult.UtcOffsetSeconds.Should()
            .Be(secondWeather.UtcOffsetSeconds);
        actualResult.Hourly.Time[0].Should()
            .Be(secondWeather.Hourly.First().Time);
        actualResult.Hourly.Temperature2M[0].Should()
            .Be(secondWeather.Hourly.First().Temperature2M);
        actualResult.Hourly.RelativeHumidity2M[0].Should()
            .Be(secondWeather.Hourly.First().RelativeHumidity2M);
        actualResult.Hourly.WindSpeed10m[0].Should()
            .Be(secondWeather.Hourly.First().WindSpeed10M);
        actualResult.HourlyUnits.Time.Should()
            .Be(secondWeather.HourlyUnit.Time);
        actualResult.HourlyUnits.Temperature2M.Should()
            .Be(secondWeather.HourlyUnit.Temperature2M);
        actualResult.HourlyUnits.RelativeHumidity2M.Should()
            .Be(secondWeather.HourlyUnit.RelativeHumidity2M);
        actualResult.HourlyUnits.WindSpeed10M.Should()
            .Be(secondWeather.HourlyUnit.WindSpeed10M);
    }

    private WeatherForecast CreateWeather(float latitude, float longitude)
    {
        var weather = new WeatherForecast
        {
            Latitude = latitude,
            Longitude = longitude,
            GenerationTimeMs = 12,
            Elevation = 12,
            UtcOffsetSeconds = 1,
            Timezone = "dummy-zone",
            TimezoneAbbreviation = "dummy-abbrev",
            HourlyUnit = new HourlyUnit
            {
                Time = "time",
                Temperature2M = "temp",
                RelativeHumidity2M = "relative",
                WindSpeed10M = "wind-speed"
            },
            Hourly = new HashSet<Hourly>
            {
                new()
                {
                    Time = DateTime.Now,
                    Temperature2M = 11,
                    RelativeHumidity2M = 11,
                    WindSpeed10M = 11
                }
            }
        };

        _dbContext.WeatherForecasts.Add(weather);
        _dbContext.SaveChanges();

        return weather;
    }
}