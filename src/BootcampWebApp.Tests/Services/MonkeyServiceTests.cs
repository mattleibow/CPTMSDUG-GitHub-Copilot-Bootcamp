using BootcampShared.Services;
using BootcampShared.Models;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace BootcampWebApp.Tests.Services;

public class MonkeyServiceTests
{
    [Fact]
    public async Task GetMonkeysAsync_WithValidResponse_ReturnsMonkeys()
    {
        // Arrange
        var testMonkeys = new[]
        {
            new Monkey { Name = "Test Monkey", Location = "Test Location", Population = 1000 }
        };
        
        var mockHttpClient = CreateMockHttpClient(testMonkeys, HttpStatusCode.OK);
        var monkeyService = new MonkeyService(mockHttpClient);

        // Act
        var result = await monkeyService.GetMonkeysAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Monkey", result[0].Name);
        Assert.Equal("Test Location", result[0].Location);
        Assert.Equal(1000, result[0].Population);
    }

    [Fact]
    public async Task GetMonkeysAsync_WithHttpError_ReturnsEmptyArray()
    {
        // Arrange
        var mockHttpClient = CreateMockHttpClient<Monkey[]>(Array.Empty<Monkey>(), HttpStatusCode.NotFound);
        var monkeyService = new MonkeyService(mockHttpClient);

        // Act
        var result = await monkeyService.GetMonkeysAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    private static HttpClient CreateMockHttpClient<T>(T responseData, HttpStatusCode statusCode)
    {
        var mockHandler = new TestHttpMessageHandler(responseData, statusCode);
        return new HttpClient(mockHandler);
    }

    private class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly object? _responseData;
        private readonly HttpStatusCode _statusCode;

        public TestHttpMessageHandler(object? responseData, HttpStatusCode statusCode)
        {
            _responseData = responseData;
            _statusCode = statusCode;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            if (_statusCode == HttpStatusCode.OK && _responseData != null)
            {
                var json = JsonSerializer.Serialize(_responseData);
                response = new HttpResponseMessage(_statusCode)
                {
                    Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
                };
            }
            else
            {
                response = new HttpResponseMessage(_statusCode);
            }

            return Task.FromResult(response);
        }
    }
}