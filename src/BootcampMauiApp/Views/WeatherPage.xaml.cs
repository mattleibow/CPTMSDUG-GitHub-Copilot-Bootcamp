using BootcampShared.Services;
using BootcampShared.Models;
using System.Collections.ObjectModel;

namespace BootcampMauiApp.Views;

public partial class WeatherPage : ContentPage
{
    private readonly IWeatherService _weatherService;
    public ObservableCollection<WeatherForecast> Forecasts { get; set; } = new();

    public WeatherPage(IWeatherService weatherService)
    {
        _weatherService = weatherService;
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadWeatherData();
    }

    private async Task LoadWeatherData()
    {
        try
        {
            LoadingLabel.IsVisible = true;
            ForecastCollectionView.IsVisible = false;

            var forecasts = await _weatherService.GetWeatherForecastsAsync();
            
            Forecasts.Clear();
            foreach (var forecast in forecasts)
            {
                Forecasts.Add(forecast);
            }

            LoadingLabel.IsVisible = false;
            ForecastCollectionView.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load weather data: {ex.Message}", "OK");
            LoadingLabel.IsVisible = false;
        }
    }
}