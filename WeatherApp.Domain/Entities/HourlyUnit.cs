namespace WeatherApp.Domain.Entities;

public class HourlyUnit
{
    public long Id { get; set; }
    public string Time { get; set; }
    public string Temperature2M { get; set; }
    public string RelativeHumidity2M { get; set; }
    public string WindSpeed10M { get; set; }

    public long WeatherForecastId { get; set; }
    public WeatherForecast WeatherForecast { get; set; }
}