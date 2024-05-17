using ChatRoom.EFCore;
using ChatRoom.GeneralSerivces;
using ChatRoom.Services;
using ChatRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
            options.UseSnakeCaseNamingConvention();
        });
    }

    // Add cookie authentication
    public static void AddCookieAuthentication(this IServiceCollection services)
    {
        // Get google section
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var googleAuthSection = configuration.GetSection("Authentication:Google") ?? throw new Exception("Google auth section error");

        services
            .AddAuthentication(
                options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
            .AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                options =>
                {
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Home/AccessDeny";
                })
            .AddGoogle(
                GoogleDefaults.AuthenticationScheme, 
                options =>
                {
                    options.ClientId = googleAuthSection["ClientId"] ?? throw new Exception("Google auth section error");
                    options.ClientSecret = googleAuthSection["ClientSecret"] ?? throw new Exception("Google auth section error");
                    options.CallbackPath = "/signin-google";  // Ensure this matche the redirect URI in Google Cloud Console
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
        services.AddScoped<AlertService>();
    }
}
