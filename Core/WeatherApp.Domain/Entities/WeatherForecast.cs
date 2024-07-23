namespace WeatherApp.Domain.Entities;

public class WeatherForecast
{
    public long Id { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public float GenerationTimeMs { get; set; }
    public float Elevation { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; }
    public string TimezoneAbbreviation { get; set; }


    public HourlyUnit HourlyUnit { get; set; }
    public HashSet<Hourly> Hourly { get; set; }
}