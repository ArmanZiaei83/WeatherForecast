using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using WeatherApp.Services.Contract.RestCall;
using WeatherApp.Services.Contract.RestCall.Dto;
using WeatherApp.Services.Contract.Weathers;
using WeatherApp.Services.Contract.Weathers.Dto;
using WeatherApp.Services.WeatherForecasts.Handlers;
using WeatherApp.UnitTest.TestTools.Builder;
using WeatherApp.UnitTest.TestTools.Factory;
using Xunit;

namespace WeatherApp.UnitTest.UnitTests.WeatherForecasts;

public class WeatherDataHandlerTests
{
    private const string Url = "https://api.open-meteo.com/v1/" +
                               "forecast?latitude=52.52&longitude=" +
                               "13.41&hourly=temperature_2m,relativehumidity_2m,windspeed_10m";

    private readonly WeatherForecastDataHandler _handler;

    private readonly Mock<IRestService> _restServiceMock;
    private readonly Mock<IWeatherService> _weatherServiceMock;


    public WeatherDataHandlerTests()
    {
        _restServiceMock = new Mock<IRestService>();
        _weatherServiceMock = new Mock<IWeatherService>();
        _handler = WeatherDataHandlerFactory.GenerateService(
            _weatherServiceMock.Object, _restServiceMock.Object);
    }

    private static int TimeOut => 3;

    [Fact]
    public async Task HandleAsync_FetchesDataFromRestClient_Properly()
    {
        var expectedResult = GenerateDto();

        _restServiceMock.Setup(_ =>
                _.GetAsync<WeatherDto>(It.Is<RestRequestDto>(_ =>
                    _.Url.Equals(Url) &&
                    _.TimeoutSeconds.Equals(TimeOut))))
            .ReturnsAsync(expectedResult);

        var actualResult = await _handler.HandleAsync();

        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(expectedResult);
        _weatherServiceMock.Verify(_ => _.Create(expectedResult));
    }

    [Fact]
    public async Task
        HandleAsync_FetchesDataFromDbWhenClientIsNotResponding_Properly()
    {
        var expectedResult = GenerateDto();

        _restServiceMock.Setup(_ =>
                _.GetAsync<WeatherDto>(It.Is<RestRequestDto>(_ =>
                    _.Url.Equals(Url) &&
                    _.TimeoutSeconds.Equals(TimeOut))))
            .ReturnsAsync(null as WeatherDto);
        _weatherServiceMock.Setup(_ => _.GetLatestData())
            .ReturnsAsync(expectedResult);

        var actualResult = await _handler.HandleAsync();

        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task
        HandleAsync_ReturnsNullWhenDbIsEmptyAndClientIsNotResponding_Properly()
    {
        var expectedResult = GenerateDto();

        _restServiceMock.Setup(_ =>
                _.GetAsync<WeatherDto>(It.Is<RestRequestDto>(_ =>
                    _.Url.Equals(Url) &&
                    _.TimeoutSeconds.Equals(TimeOut))))
            .ReturnsAsync(null as WeatherDto);
        _weatherServiceMock.Setup(_ => _.GetLatestData())
            .ReturnsAsync(null as WeatherDto);

        var actualResult = await _handler.HandleAsync();

        actualResult.Should().BeNull();
    }


    private static WeatherDto GenerateDto()
    {
        return new WeatherDtoBuilder()
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
                .WithTime(new[] { DateTime.Now })
                .WithTemperature2M(new[] { 1.1F })
                .WithRelativeHumidity2M(new[] { 1.1F })
                .WithWindSpeed10M(new[] { 1.1F }).Build())
            .Build();
    }
}