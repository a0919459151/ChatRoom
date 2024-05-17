using ChatRoom.EFCore.DBEntities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ChatRoom.GeneralSerivces;

public class HttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // CookieLogin
    public async Task CookieLogin(User user)
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new Exception("HttpContext not found");

        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.NickName ?? user.Account),
        ];

        var claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    // CookieLogout
    public async Task CookieLogout()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new Exception("HttpContext not found");

        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    // GetCurrentUserId
    public Guid GetCurrentUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new Exception("HttpContext not found");

        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new Exception("Claim nameid not found");

        return Guid.Parse(userId);
    }

    // GetCurrentUserName
    public string GetCurrentUserName()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new Exception("HttpContext not found");

        var userName = httpContext.User.FindFirst(ClaimTypes.Name)?.Value
            ?? throw new Exception("Claim name not found");

        return userName;
    }
}
