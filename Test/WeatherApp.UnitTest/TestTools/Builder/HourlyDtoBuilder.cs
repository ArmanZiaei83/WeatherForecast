using System;
using WeatherApp.Services.Contract.Weathers.Dto;

namespace WeatherApp.UnitTest.TestTools.Builder;

public class HourlyDtoBuilder
{
    private readonly HourlyDto _dto;

    public HourlyDtoBuilder()
    {
        _dto = new HourlyDto();
    }

    public HourlyDtoBuilder WithTime(DateTime[] time)
    {
        _dto.Time = time;
        return this;
    }

    public HourlyDtoBuilder WithRelativeHumidity2M(
        float[] relativeHumidity2M)
    {
        _dto.RelativeHumidity2M = relativeHumidity2M;
        return this;
    }

    public HourlyDtoBuilder WithWindSpeed10M(float[] windSpeed10M)
    {
        _dto.WindSpeed10m = windSpeed10M;
        return this;
    }

    public HourlyDtoBuilder WithTemperature2M(float[] temperature2M)
    {
        _dto.Temperature2M = temperature2M;
        return this;
    }

    public HourlyDto Build()
    {
        return _dto;
    }
}