using WeatherApp.Services.Contract.RestCall.Dto;

namespace WeatherApp.Services.Contract.RestCall;

public interface IRestService
{
    public Task<T> GetAsync<T>(RestRequestDto requestDto)
        where T : class;
}