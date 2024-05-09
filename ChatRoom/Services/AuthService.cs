using ChatRoom.Conracts.Auth;
using ChatRoom.EFCore;
using ChatRoom.EFCore.DBEntities;
using ChatRoom.Services.GeneralSerivces;
using ChatRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly HttpContextService _httpContextService;

    public AuthService(
        AppDbContext context,
        HttpContextService httpContextService)
    {
        _context = context;
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

        // Add user to db
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        // CookieLogin
        await _httpContextService.CookieLogin(newUser);

        return true;
    }

    public async Task Logout()
    {
        // Get current user id
        Guid userId = _httpContextService.GetCurrentUserId();

        // Query
        User user = await _context.Users
            .Where(u => u.Id == userId)
            .FirstAsync();

        // Remove
        _context.Users.Remove(user);
        
        // Save
        await _context.SaveChangesAsync();

        // CookieLogout
        await _httpContextService.CookieLogout();
    }
}
