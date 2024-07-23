using WeatherApp.Services.Contract.Weathers.Dto;

namespace WeatherApp.UnitTest.TestTools.Builder;

public class HourlyUnitDtoBuilder
{
    private readonly HourlyUnitDto _dto;

    public HourlyUnitDtoBuilder()
    {
        _dto = new HourlyUnitDto();
    }

    public HourlyUnitDtoBuilder WithTime(string time)
    {
        _dto.Time = time;
        return this;
    }

    public HourlyUnitDtoBuilder WithRelativeHumidity2M(
        string relativeHumidity2M)
    {
        _dto.RelativeHumidity2M = relativeHumidity2M;
        return this;
    }

    public HourlyUnitDtoBuilder WithWindSpeed10M(string windSpeed10M)
    {
        _dto.WindSpeed10M = windSpeed10M;
        return this;
    }

    public HourlyUnitDtoBuilder WithTemperature2M(string temperature2M)
    {
        _dto.Temperature2M = temperature2M;
        return this;
    }

    public HourlyUnitDto Build()
    {
        return _dto;
    }
}