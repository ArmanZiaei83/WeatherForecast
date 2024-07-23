using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.Protected;
using WeatherApp.Services.Contract.RestCall;
using WeatherApp.Services.Contract.RestCall.Dto;
using WeatherApp.Services.RestCall;
using Xunit;

namespace WeatherApp.UnitTest.UnitTests.RestCall;

public class RestAppServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly IRestService _restService;

    public RestAppServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _restService = new RestAppService(httpClient);
    }

    private static string ExampleUrl => "https://example.com";
    private static string ResponseContent => "{\"key\": \"value\"}";

    private static int Timeout => 2;

    [Fact]
    public async Task GetAsync_ShouldReturnData_Properly()
    {
        var expectedResponse = ResponseContent;
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(expectedResponse)
        };
        MockHttpClientResponseToReturn(httpResponseMessage);

        var actualResult =
            await _restService.GetAsync<Dictionary<string, string>>(
                new RestRequestDto
                {
                    Url = ExampleUrl,
                    TimeoutSeconds = Timeout
                });

        actualResult.Should().NotBeNull();
        actualResult["key"].Should().Be("value");
    }

    [Fact]
    public async Task GetAsync_StopsRequestAfterTimeout()
    {
        var requestDto = new RestRequestDto
        {
            Url = ExampleUrl,
            TimeoutSeconds = Timeout
        };
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage _,
                CancellationToken cancellationToken) =>
            {
                Task.Delay(TimeSpan.FromSeconds(3), cancellationToken)
                    .Wait(cancellationToken);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(ResponseContent)
                };
            });

        var actualResult = () => _restService.GetAsync<object>(requestDto);

        await actualResult.Should()
            .ThrowExactlyAsync<TaskCanceledException>();
    }


    private void MockHttpClientResponseToReturn(
        HttpResponseMessage httpResponseMessage)
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);
    }
}