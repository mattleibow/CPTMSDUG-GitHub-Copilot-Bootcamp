using BootcampWebApp.Models;

namespace BootcampWebApp.Services;

public class MonkeyService
{
    private readonly HttpClient _httpClient;
    private const string MonkeysApiUrl = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

    public MonkeyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Monkey[]> GetMonkeysAsync()
    {
        try
        {
            var monkeys = await _httpClient.GetFromJsonAsync<Monkey[]>(MonkeysApiUrl);
            return monkeys ?? [];
        }
        catch (Exception)
        {
            // Return empty array on error
            return [];
        }
    }
}