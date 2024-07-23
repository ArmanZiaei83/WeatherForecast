using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Services.Contract.RestCall;
using WeatherApp.Services.Contract.Weathers;
using WeatherApp.Services.RestCall;
using WeatherApp.Services.WeatherForecasts.Handlers;
using WeatherApp.Services.WeatherForecasts.Services;

namespace WeatherApp.Services;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IWeatherService, WeatherForecastAppService>();
        services.AddScoped<IRestService, RestAppService>();
        services.AddScoped<WeatherForecastDataHandler>();
    }
}