using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Persistence.Context;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public DbSet<Hourly> Hourlies { get; set; }
    public DbSet<HourlyUnit> HourlyUnits { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());
    }
}