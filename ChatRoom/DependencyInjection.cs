using ChatRoom.Services;
using ChatRoom.Services.GeneralSerivces;
using ChatRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DependencyInjection;

public static class DependencyInjection
{
    // Add cookie authentication
    public static void AddCookieAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Home/AccessDeny";
            });
    }

    // AddSerivces
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IChatRoomService, ChatRoomService>();
    }

    // AddGeneralSerivces
    public static void AddGeneralServices(this IServiceCollection services)
    {
        services.AddScoped<HttpContextService>();
    }

}
