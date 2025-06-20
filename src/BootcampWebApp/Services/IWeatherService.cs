using BootcampWebApp.Models;

namespace BootcampWebApp.Services;

public interface IWeatherService
{
    Task<WeatherForecast[]> GetForecastAsync();
}