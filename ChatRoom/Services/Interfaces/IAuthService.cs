using ChatRoom.Contracts.Auth;

namespace ChatRoom.Services.Interfaces;

public interface IAuthService
{
    Task Signup(SignupViewModel model);

    Task Login(LoginViewModel model);

    Task Logout();
}
