using BootcampShared.Models;
using System.Net.Http.Json;

namespace BootcampShared.Services;

public class MonkeyService : IMonkeyService
{
    private readonly HttpClient _httpClient;

    public MonkeyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Monkey[]> GetMonkeysAsync()
    {
        try
        {
            // Fetch monkey data from the specified URL
            var monkeys = await _httpClient.GetFromJsonAsync<Monkey[]>("https://montemagno.com/monkeys.json");
            return monkeys ?? Array.Empty<Monkey>();
        }
        catch (Exception ex)
        {
            // Handle any errors gracefully
            Console.WriteLine($"Error loading monkeys: {ex.Message}");
            return Array.Empty<Monkey>();
        }
    }
}