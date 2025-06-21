using BootcampShared.Models;

namespace BootcampShared.Services;

public interface IMonkeyService
{
    Task<Monkey[]> GetMonkeysAsync();
}