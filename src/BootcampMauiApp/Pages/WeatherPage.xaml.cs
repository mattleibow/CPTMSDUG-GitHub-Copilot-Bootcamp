using BootcampShared.Services;
using BootcampShared.Models;

namespace BootcampMauiApp.Pages;

public partial class WeatherPage : ContentPage
{
	private readonly IWeatherService _weatherService;

	public WeatherPage(IWeatherService weatherService)
	{
		InitializeComponent();
		_weatherService = weatherService;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await LoadWeatherDataAsync();
	}

	private async Task LoadWeatherDataAsync()
	{
		try
		{
			var forecasts = await _weatherService.GetWeatherForecastsAsync();
			
			LoadingLabel.IsVisible = false;
			WeatherCollectionView.ItemsSource = forecasts;
			WeatherCollectionView.IsVisible = true;
		}
		catch (Exception ex)
		{
			LoadingLabel.Text = $"Error loading weather data: {ex.Message}";
		}
	}
}