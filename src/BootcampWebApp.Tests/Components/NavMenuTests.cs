using Bunit;
using BootcampWebApp.Components.Layout;

namespace BootcampWebApp.Tests.Components;

public class NavMenuTests : TestContext
{
    [Fact]
    public void NavMenu_MonkeysPage_HasMonkeyIcon()
    {
        // Act
        var component = RenderComponent<NavMenu>();

        // Assert
        Assert.Contains("bi-monkey-nav-menu", component.Markup);
    }

    [Fact]
    public void NavMenu_WeatherPage_HasListIcon()
    {
        // Act
        var component = RenderComponent<NavMenu>();

        // Assert
        Assert.Contains("bi-list-nested-nav-menu", component.Markup);
    }

    [Fact]
    public void NavMenu_AllNavigationItems_ArePresent()
    {
        // Act
        var component = RenderComponent<NavMenu>();

        // Assert
        Assert.Contains("Home", component.Markup);
        Assert.Contains("Counter", component.Markup);
        Assert.Contains("Weather", component.Markup);
        Assert.Contains("Monkeys", component.Markup);
    }
}