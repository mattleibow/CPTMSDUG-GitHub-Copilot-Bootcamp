using BootcampShared.Models;

namespace BootcampShared.Services;

public interface IWeatherService
{
    Task<WeatherForecast[]> GetWeatherForecastsAsync();
}