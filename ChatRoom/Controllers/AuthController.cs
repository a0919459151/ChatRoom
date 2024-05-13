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

    // Sign up page
    public IActionResult Signup()
    {
        return View(new SignupViewModel());
    }

    // Sign up
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signup(SignupViewModel model)
    {
        await _authService.Signup(model);

        return RedirectToAction("Login", "Auth");
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
        await _authService.Login(model);

        return RedirectToAction("Index", "ChatRoom");
    }

    // Logout
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();

        return RedirectToAction(nameof(Login));
    }
}
