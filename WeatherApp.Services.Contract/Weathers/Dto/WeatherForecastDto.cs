using System.Text.Json.Serialization;

namespace WeatherApp.Services.Contract.Weathers.Dto;

public class WeatherDto
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public float GenerationTimeMs { get; set; }

    public float Elevation { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    public string Timezone { get; set; }

    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; }

    [JsonPropertyName("hourly_units")]
    public HourlyUnitDto HourlyUnits { get; set; }

    [JsonPropertyName("hourly")] public HourlyDto Hourly { get; set; }
}