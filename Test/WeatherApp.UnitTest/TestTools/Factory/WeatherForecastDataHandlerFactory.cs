using WeatherApp.Services.Contract.RestCall;
using WeatherApp.Services.Contract.Weathers;
using WeatherApp.Services.WeatherForecasts.Handlers;

namespace WeatherApp.UnitTest.TestTools.Factory;

public static class WeatherDataHandlerFactory
{
    public static WeatherForecastDataHandler GenerateService(
        IWeatherService weatherService, IRestService restService)
    {
        return new WeatherForecastDataHandler(weatherService, restService);
    }
}