using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.Services.Contract;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}