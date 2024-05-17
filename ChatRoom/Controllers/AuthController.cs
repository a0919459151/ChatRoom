using ChatRoom.Contracts.Auth;
using ChatRoom.GeneralSerivces;
using ChatRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers;

[AllowAnonymous]
public class AuthController : Controller
{
    private readonly AlertService _alertService;
    private readonly IAuthService _authService;

    public AuthController(
        AlertService alertService,
        IAuthService authService)
    {
        _alertService = alertService;
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

        _alertService.Success("Sign up success!");

        return RedirectToAction("Login");
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

    [HttpGet]
    public IActionResult ExternalLogin(string provider)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Auth");

        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

        return Challenge(properties, provider);
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (result?.Principal == null)
        {
            // Authentication failed
            return RedirectToAction("Login");
        }

        // The user is authenticated
        // You can perform additional logic here, like logging or fetching more user details
        return RedirectToAction("Index", "ChatRoom");
    }

    // Logout
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();

        return RedirectToAction(nameof(Login));
    }
}
