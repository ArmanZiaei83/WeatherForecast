using WeatherApp.Services.Contract.RestCall;
using WeatherApp.Services.Contract.RestCall.Dto;
using WeatherApp.Services.Contract.Weathers;
using WeatherApp.Services.Contract.Weathers.Dto;

namespace WeatherApp.Services.WeatherForecasts.Handlers;

public class WeatherForecastDataHandler
{
    private readonly IRestService _restService;
    private readonly IWeatherService _weatherService;

    private static string Url =>
        "https://api.open-meteo.com/v1/" +
        "forecast?latitude=52.52&longitude=" +
        "13.41&hourly=temperature_2m,relativehumidity_2m,windspeed_10m";

    public WeatherForecastDataHandler(
        IWeatherService weatherService,
        IRestService restService)
    {
        _weatherService = weatherService;
        _restService = restService;
    }

    public async Task<WeatherDto?> HandleAsync()
    {
        var queriedWeather = await GetWeatherFromRestServiceAsync();

        if (queriedWeather == null) return await GetLastSavedWeatherAsync();

        await SaveWeatherAsync(queriedWeather);
        return queriedWeather;
    }

    private async Task<WeatherDto?> GetWeatherFromRestServiceAsync()
    {
        var restRequest = new RestRequestDto
        {
            Url = Url,
            TimeoutSeconds = 2
        };

        return await _restService.GetAsync<WeatherDto>(restRequest);
    }

    private async Task SaveWeatherAsync(WeatherDto weather)
    {
        await _weatherService.Create(weather);
    }

    private async Task<WeatherDto?> GetLastSavedWeatherAsync()
    {
        return await _weatherService.GetLatestData();
    }
}