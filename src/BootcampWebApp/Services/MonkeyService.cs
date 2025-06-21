using BootcampWebApp.Models;

namespace BootcampWebApp.Services;

public class MonkeyService
{
    private readonly HttpClient _httpClient;
    private const string MonkeysUrl = "https://montemagno.com/monkeys.json";

    public MonkeyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Monkey>> GetMonkeysAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(MonkeysUrl);
            response.EnsureSuccessStatusCode();
            
            var monkeys = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            return monkeys ?? new List<Monkey>();
        }
        catch
        {
            // Return empty list on error
            return new List<Monkey>();
        }
    }
}