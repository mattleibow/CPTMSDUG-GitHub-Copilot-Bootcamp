using BootcampWebApp.Services;
using BootcampWebApp.Models;
using Moq;
using Moq.Protected;
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
        var mockHandler = new Mock<HttpMessageHandler>();
        
        HttpResponseMessage response;
        if (statusCode == HttpStatusCode.OK && responseData != null)
        {
            var json = JsonSerializer.Serialize(responseData);
            response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
        }
        else
        {
            response = new HttpResponseMessage(statusCode);
        }

        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        return new HttpClient(mockHandler.Object);
    }
}