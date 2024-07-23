using System.Text.Json.Serialization;

namespace WeatherApp.Services.Contract.Weathers.Dto;

public class HourlyUnitDto
{
    public string Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public string Temperature2M { get; set; }

    [JsonPropertyName("relativehumidity_2m")]
    public string RelativeHumidity2M { get; set; }

    [JsonPropertyName("windspeed_10m")] public string WindSpeed10M { get; set; }
}