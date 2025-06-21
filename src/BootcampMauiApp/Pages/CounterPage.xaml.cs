namespace BootcampMauiApp.Pages;

public partial class CounterPage : ContentPage
{
	int count = 0;

	public CounterPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;
		CounterLabel.Text = $"Current count: {count}";
		SemanticScreenReader.Announce(CounterLabel.Text);
	}
}