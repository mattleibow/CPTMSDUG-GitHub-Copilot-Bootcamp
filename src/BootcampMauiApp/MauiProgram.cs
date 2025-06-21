using BootcampShared.Services;
using BootcampMauiApp.Views;
using Microsoft.Extensions.Logging;

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

        // Register application services
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        builder.Services.AddHttpClient<IMonkeyService, MonkeyService>();

        // Register pages
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<CounterPage>();
        builder.Services.AddTransient<WeatherPage>();
        builder.Services.AddTransient<MonkeysPage>();

#if DEBUG
        builder.Services.AddLogging(logging =>
        {
            logging.AddDebug();
        });
#endif

        return builder.Build();
    }
}