namespace WeatherApp.Services.Contract.RestCall.Dto;

public class RestRequestDto
{
    public string Url { get; set; }
    public long TimeoutSeconds { get; set; }
}