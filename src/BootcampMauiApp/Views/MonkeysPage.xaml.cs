using BootcampShared.Services;
using BootcampShared.Models;
using System.Collections.ObjectModel;

namespace BootcampMauiApp.Views;

public partial class MonkeysPage : ContentPage
{
    private readonly IMonkeyService _monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; set; } = new();

    public MonkeysPage(IMonkeyService monkeyService)
    {
        _monkeyService = monkeyService;
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMonkeyData();
    }

    private async Task LoadMonkeyData()
    {
        try
        {
            LoadingLabel.IsVisible = true;
            MonkeyCollectionView.IsVisible = false;

            var monkeys = await _monkeyService.GetMonkeysAsync();
            
            Monkeys.Clear();
            foreach (var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }

            LoadingLabel.IsVisible = false;
            MonkeyCollectionView.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load monkey data: {ex.Message}", "OK");
            LoadingLabel.IsVisible = false;
        }
    }
}