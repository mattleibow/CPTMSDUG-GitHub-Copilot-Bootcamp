using BootcampShared.Services;
using BootcampShared.Models;

namespace BootcampMauiApp.Pages;

public partial class MonkeysPage : ContentPage
{
	private readonly IMonkeyService _monkeyService;

	public MonkeysPage(IMonkeyService monkeyService)
	{
		InitializeComponent();
		_monkeyService = monkeyService;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await LoadMonkeyDataAsync();
	}

	private async Task LoadMonkeyDataAsync()
	{
		try
		{
			var monkeys = await _monkeyService.GetMonkeysAsync();
			
			LoadingLabel.IsVisible = false;
			MonkeysCollectionView.ItemsSource = monkeys;
			MonkeysCollectionView.IsVisible = true;
		}
		catch (Exception ex)
		{
			LoadingLabel.Text = $"Error loading monkey data: {ex.Message}";
		}
	}
}