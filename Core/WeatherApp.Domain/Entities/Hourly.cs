namespace WeatherApp.Domain.Entities;

public class Hourly
{
    public long Id { get; set; }
    public DateTime Time { get; set; }
    public float Temperature2M { get; set; }
    public float RelativeHumidity2M { get; set; }
    public float WindSpeed10M { get; set; }

    public long WeatherForecastId { get; set; }
    public WeatherForecast WeatherForecast { get; set; }
}