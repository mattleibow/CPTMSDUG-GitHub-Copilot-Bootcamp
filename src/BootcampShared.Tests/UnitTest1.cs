using BootcampShared.Services;
using BootcampShared.Models;

namespace BootcampShared.Tests.Services;

public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeatherForecastsAsync_ReturnsValidForecasts()
    {
        // Arrange
        var weatherService = new WeatherService();

        // Act
        var forecasts = await weatherService.GetWeatherForecastsAsync();

        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Length);
        Assert.All(forecasts, forecast =>
        {
            Assert.True(forecast.Date > DateOnly.FromDateTime(DateTime.Now));
            Assert.InRange(forecast.TemperatureC, -20, 55);
            Assert.NotNull(forecast.Summary);
            Assert.NotEqual(0, forecast.TemperatureF); // Just verify it's computed
        });
    }

    [Fact]
    public async Task GetWeatherForecastsAsync_TakesAppropriateTime()
    {
        // Arrange
        var weatherService = new WeatherService();
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        // Act
        await weatherService.GetWeatherForecastsAsync();

        // Assert
        stopwatch.Stop();
        Assert.True(stopwatch.ElapsedMilliseconds >= 500, "Service should simulate delay");
    }
}
