using System.Text.Json;
using WeatherApp.Services.Contract.RestCall;
using WeatherApp.Services.Contract.RestCall.Dto;

namespace WeatherApp.Services.RestCall;

public class RestAppService : IRestService
{
    private readonly HttpClient _httpClient;

    public RestAppService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(
        RestRequestDto requestDto) where T : class
    {
        _httpClient.Timeout = TimeSpan.FromSeconds(requestDto.TimeoutSeconds);
        var response = await _httpClient.GetAsync(requestDto.Url);

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<T>(
            responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}