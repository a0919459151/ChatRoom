using ChatRoom.Contracts.Auth;

namespace ChatRoom.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Login(LoginViewModel model);
    Task Logout();
}
