using BootcampWebApp.Models;
using System.Text.Json;

namespace BootcampWebApp.Services;

public class MonkeyService
{
    private readonly HttpClient _httpClient;
    private const string MonkeyDataUrl = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

    public MonkeyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Monkey>> GetMonkeysAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(MonkeyDataUrl);
            var monkeys = JsonSerializer.Deserialize<List<Monkey>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            return monkeys ?? new List<Monkey>();
        }
        catch
        {
            // Return empty list if there's an error fetching data
            return new List<Monkey>();
        }
    }
}