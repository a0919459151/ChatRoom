using ChatRoom.Contracts.Auth;
using ChatRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers;

[AllowAnonymous]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    // Login page
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    // Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var isSuccess = await _authService.Login(model);

        if (!isSuccess)
        {
            return View(model);
        }

        return RedirectToAction("Index", "ChatRoom");
    }

    // Logout
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return RedirectToAction(nameof(Login));
    }
}
