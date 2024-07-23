using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Persistence.Weathers;

public class HourlyUnitEntityMap : IEntityTypeConfiguration<HourlyUnit>
{
    public void Configure(EntityTypeBuilder<HourlyUnit> entity)
    {
        entity.ToTable("HourlyUnits");
        entity.HasKey(u => u.Id);
        entity.Property(u => u.Id).ValueGeneratedOnAdd();

        entity.Property(u => u.Time).IsRequired();
        entity.Property(u => u.Temperature2M).IsRequired();
        entity.Property(u => u.RelativeHumidity2M).IsRequired();
        entity.Property(u => u.WindSpeed10M).IsRequired();
        entity.Property(u => u.WeatherForecastId).IsRequired();
    }
}