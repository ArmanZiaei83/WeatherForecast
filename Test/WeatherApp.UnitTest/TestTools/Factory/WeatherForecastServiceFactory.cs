using WeatherApp.Persistence;
using WeatherApp.Persistence.Context;
using WeatherApp.Persistence.Weathers;
using WeatherApp.Services.WeatherForecasts.Services;

namespace WeatherApp.UnitTest.TestTools.Factory;

public static class WeatherServiceFactory
{
    public static WeatherForecastAppService GenerateService(
        DataContext dataContext)
    {
        var unitOfWork = new UnitOfWork(dataContext);
        var repository = new WeatherRepository(dataContext);
        var service = new WeatherForecastAppService(repository, unitOfWork);
        return service;
    }
}