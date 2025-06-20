using Bunit;
using BootcampWebApp.Components.Pages;
using BootcampWebApp.Services;
using BootcampWebApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BootcampWebApp.Tests.Components;

public class MonkeysPageTests : TestContext
{
    [Fact]
    public void MonkeysPage_WithValidData_RendersCorrectly()
    {
        // Arrange
        var testMonkeys = new[]
        {
            new Monkey 
            { 
                Name = "Barbary Ape", 
                Location = "Gibraltar", 
                Details = "Smart monkey",
                Population = 300,
                Image = "https://example.com/monkey.jpg"
            },
            new Monkey 
            { 
                Name = "Spider Monkey", 
                Location = "Central America", 
                Details = "Very agile",
                Population = 2000,
                Image = "https://example.com/spider.jpg"
            }
        };

        var mockMonkeyService = new Mock<IMonkeyService>();
        mockMonkeyService.Setup(x => x.GetMonkeysAsync())
                        .ReturnsAsync(testMonkeys);

        Services.AddSingleton(mockMonkeyService.Object);

        // Act
        var component = RenderComponent<Monkeys>();

        // Assert
        Assert.Contains("Amazing Monkeys", component.Markup);
        Assert.Contains("Barbary Ape", component.Markup);
        Assert.Contains("Spider Monkey", component.Markup);
        Assert.Contains("Gibraltar", component.Markup);
        Assert.Contains("Central America", component.Markup);
        Assert.Contains("300", component.Markup);
        Assert.Contains("2,000", component.Markup);
    }

    [Fact]
    public void MonkeysPage_InitiallyShowsLoading()
    {
        // Arrange
        var mockMonkeyService = new Mock<IMonkeyService>();
        // Setup a long-running task to keep loading state
        mockMonkeyService.Setup(x => x.GetMonkeysAsync())
                        .Returns(Task.Delay(1000).ContinueWith<Monkey[]>(_ => new Monkey[0]));

        Services.AddSingleton(mockMonkeyService.Object);

        // Act
        var component = RenderComponent<Monkeys>();

        // Assert
        Assert.Contains("Loading amazing monkeys...", component.Markup);
    }

    [Fact]
    public void MonkeysPage_WithEmptyData_RendersEmptyGrid()
    {
        // Arrange
        var mockMonkeyService = new Mock<IMonkeyService>();
        mockMonkeyService.Setup(x => x.GetMonkeysAsync())
                        .ReturnsAsync(Array.Empty<Monkey>());

        Services.AddSingleton(mockMonkeyService.Object);

        // Act
        var component = RenderComponent<Monkeys>();

        // Assert
        Assert.Contains("Amazing Monkeys", component.Markup);
        Assert.DoesNotContain("Loading amazing monkeys...", component.Markup);
    }
}