using WeatherApp.Services.Contract.Weathers.Dto;

namespace WeatherApp.UnitTest.TestTools.Builder;

public class WeatherDtoBuilder
{
    private readonly WeatherDto _dto;

    public WeatherDtoBuilder()
    {
        _dto = new WeatherDto();
    }

    public WeatherDtoBuilder WithLatitude(float latitude)
    {
        _dto.Latitude = latitude;
        return this;
    }

    public WeatherDtoBuilder WithLongitude(float longitude)
    {
        _dto.Longitude = longitude;
        return this;
    }

    public WeatherDtoBuilder WithGenerationTimeMs(float generationTimeMs)
    {
        _dto.GenerationTimeMs = generationTimeMs;
        return this;
    }

    public WeatherDtoBuilder WithElevation(float elevation)
    {
        _dto.Elevation = elevation;
        return this;
    }


    public WeatherDtoBuilder WithUtcOffsetSeconds(int utcOffsetSeconds)
    {
        _dto.UtcOffsetSeconds = utcOffsetSeconds;
        return this;
    }

    public WeatherDtoBuilder WithTimezone(string timezone)
    {
        _dto.Timezone = timezone;
        return this;
    }

    public WeatherDtoBuilder WithTimezoneAbbreviation(
        string timezoneAbbreviation)
    {
        _dto.TimezoneAbbreviation = timezoneAbbreviation;
        return this;
    }

    public WeatherDtoBuilder WithHourlyUnits(HourlyUnitDto hourlyUnits)
    {
        _dto.HourlyUnits = hourlyUnits;
        return this;
    }

    public WeatherDtoBuilder WithHourly(HourlyDto hourly)
    {
        _dto.Hourly = hourly;
        return this;
    }

    public WeatherDto Build()
    {
        return _dto;
    }
}