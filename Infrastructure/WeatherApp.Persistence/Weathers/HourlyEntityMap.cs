using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Persistence.Weathers;

public class HourlyDataEntityMap : IEntityTypeConfiguration<Hourly>
{
    public void Configure(EntityTypeBuilder<Hourly> entity)
    {
        entity.ToTable("Hourly");
        entity.HasKey(d => d.Id);
        entity.Property(u => u.Id).ValueGeneratedOnAdd();

        entity.Property(d => d.Time).IsRequired();
        entity.Property(d => d.Temperature2M).IsRequired();
        entity.Property(d => d.RelativeHumidity2M).IsRequired();
        entity.Property(d => d.WindSpeed10M).IsRequired();
        entity.Property(d => d.WeatherForecastId).IsRequired();
    }
}