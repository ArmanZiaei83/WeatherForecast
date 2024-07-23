using System.Text.Json.Serialization;

namespace WeatherApp.Services.Contract.Weathers.Dto;

public class HourlyDto
{
    public DateTime[] Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public float[] Temperature2M { get; set; }

    [JsonPropertyName("relativehumidity_2m")]
    public float[] RelativeHumidity2M { get; set; }

    [JsonPropertyName("windspeed_10m")]
    public float[] WindSpeed10m { get; set; }
}