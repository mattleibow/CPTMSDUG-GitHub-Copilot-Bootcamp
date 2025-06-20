using BootcampWebApp.Models;

namespace BootcampWebApp.Services;

public interface IMonkeyService
{
    Task<Monkey[]> GetMonkeysAsync();
}