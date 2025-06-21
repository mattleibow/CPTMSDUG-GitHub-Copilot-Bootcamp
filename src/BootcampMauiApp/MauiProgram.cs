using Microsoft.Extensions.Logging;
using BootcampShared.Services;
using BootcampMauiApp.Pages;

namespace BootcampMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Register HTTP client and services
		builder.Services.AddHttpClient();
		builder.Services.AddScoped<IWeatherService, WeatherService>();
		builder.Services.AddHttpClient<IMonkeyService, MonkeyService>();

		// Register pages
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<CounterPage>();
		builder.Services.AddTransient<WeatherPage>();
		builder.Services.AddTransient<MonkeysPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
