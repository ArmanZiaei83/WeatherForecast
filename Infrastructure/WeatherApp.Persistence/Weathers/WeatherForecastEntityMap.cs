using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Persistence.Weathers;

public class WeatherEntityMap : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> entity)
    {
        entity.ToTable("WeatherForecasts");
        entity.HasKey(w => w.Id);
        entity.Property(w => w.Id).ValueGeneratedOnAdd();

        entity.Property(w => w.Latitude).IsRequired();
        entity.Property(w => w.Longitude).IsRequired();
        entity.Property(w => w.GenerationTimeMs).IsRequired();
        entity.Property(w => w.UtcOffsetSeconds).IsRequired();
        entity.Property(w => w.Timezone).IsRequired();
        entity.Property(w => w.TimezoneAbbreviation).IsRequired();
        entity.Property(w => w.Elevation).IsRequired();

        entity.HasOne(w => w.HourlyUnit)
            .WithOne(u => u.WeatherForecast)
            .HasForeignKey<HourlyUnit>(u => u.WeatherForecastId);

        entity.HasMany(w => w.Hourly)
            .WithOne(d => d.WeatherForecast)
            .HasForeignKey(d => d.WeatherForecastId);
    }
}