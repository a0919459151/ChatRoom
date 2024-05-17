using ChatRoom.Contracts.Auth;
using ChatRoom.EFCore;
using ChatRoom.EFCore.DBEntities;
using ChatRoom.GeneralSerivces;
using ChatRoom.Services.Interfaces;
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

    // Sign up
    public async Task Signup(SignupViewModel model)
    {
        // Create user
        User user = new()
        {
            Account = model.Account!,
            Password = model.Password!
        };

        // Add
        await _context.Users.AddAsync(user);

        // Save
        await _context.SaveChangesAsync();
    }

    public async Task Login(LoginViewModel model)
    {
        // TODO: avoid plain text password

        // Query
        var user = await _context.Users
            .Where(u => u.Account == model.Account)
            .Where(u => u.Password == model.Password)
            .FirstOrDefaultAsync();
        
        // Not found
        if (user is null)
            throw new Exception("User not found");

        // CookieLogin
        await _httpContextService.CookieLogin(user);
    }

    public async Task Logout()
    {
        // CookieLogout
        await _httpContextService.CookieLogout();
    }
}
