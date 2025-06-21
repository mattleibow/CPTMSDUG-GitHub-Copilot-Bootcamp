namespace BootcampMauiApp.Views;

public partial class CounterPage : ContentPage
{
    private int currentCount = 0;

    public CounterPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        currentCount++;
        CounterLabel.Text = $"Current count: {currentCount}";
    }
}