using ChatRoom.Contracts.Auth;
using ChatRoom.EFCore.DBEntities;
using ChatRoom.Services.GeneralSerivces;
using ChatRoom.Services.Interfaces;

namespace ChatRoom.Services;

public class AuthService : IAuthService
{
    private readonly HttpContextService _httpContextService;

    public AuthService(
        HttpContextService httpContextService)
    {
        _httpContextService = httpContextService;
    }

    public async Task<bool> Login(LoginViewModel model)
    {
        // New user
        User newUser = new()
        {
            Id = Guid.NewGuid(),
            NickName = model.NickName!
        };

        // CookieLogin
        await _httpContextService.CookieLogin(newUser);

        return true;
    }

    public async Task Logout()
    {
        // CookieLogout
        await _httpContextService.CookieLogout();
    }
}
