using Bunit;
using BootcampWebApp.Components.Pages;
using BootcampWebApp.Services;
using BootcampWebApp.Models;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace BootcampWebApp.Tests.Components;

public class WeatherPageTests : TestContext
{
    [Fact]
    public void WeatherPage_WithValidData_RendersCorrectly()
    {
        // Arrange
        var testForecasts = new[]
        {
            new WeatherForecast 
            { 
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 
                TemperatureC = 20, 
                Summary = "Mild" 
            },
            new WeatherForecast 
            { 
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 
                TemperatureC = 25, 
                Summary = "Warm" 
            }
        };

        var mockWeatherService = Substitute.For<IWeatherService>();
        mockWeatherService.GetWeatherForecastsAsync().Returns(testForecasts);

        Services.AddSingleton(mockWeatherService);

        // Act
        var component = RenderComponent<Weather>();

        // Assert
        Assert.Contains("Weather", component.Markup);
        Assert.Contains("Mild", component.Markup);
        Assert.Contains("Warm", component.Markup);
        Assert.Contains("20", component.Markup);
        Assert.Contains("25", component.Markup);
    }

    [Fact]
    public void WeatherPage_InitiallyShowsLoading()
    {
        // Arrange
        var mockWeatherService = Substitute.For<IWeatherService>();
        // Setup a long-running task to keep loading state
        mockWeatherService.GetWeatherForecastsAsync()
                         .Returns(Task.Delay(1000).ContinueWith<WeatherForecast[]>(_ => new WeatherForecast[0]));

        Services.AddSingleton(mockWeatherService);

        // Act
        var component = RenderComponent<Weather>();

        // Assert
        Assert.Contains("Loading...", component.Markup);
    }
}