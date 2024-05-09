using ChatRoom.EFCore;
using ChatRoom.Services;
using ChatRoom.Services.GeneralSerivces;
using ChatRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjection;

public static class DependencyInjection
{
    // Add dbconext
    public static void AddDbContext(this IServiceCollection services)
    {
        // Get connection string
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }

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
