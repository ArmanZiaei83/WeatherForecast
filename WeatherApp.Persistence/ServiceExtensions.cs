using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Persistence.Context;
using WeatherApp.Persistence.Weathers;
using WeatherApp.Services.Contract.Base.Repositories;
using WeatherApp.Services.Contract.RestCall;
using WeatherApp.Services.Contract.Weathers;
using WeatherApp.Services.RestCall;
using WeatherApp.Services.WeatherForecasts.Handlers;
using WeatherApp.Services.WeatherForecasts.Services;

namespace WeatherApp.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DbConnectionString");
        services.AddDbContext<DataContext>(opt =>
            opt.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWeatherRepository, WeatherRepository>();
    }
}