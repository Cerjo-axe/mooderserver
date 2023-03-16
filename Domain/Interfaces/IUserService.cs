using DTO;

namespace Domain.Interfaces;

public interface IUserService
{
    Task<bool> Register(RegisterDTO newuser);
    Task<bool> CheckUserExists(string email);
    Task<string> Login(LoginDTO loguser);
    Task Logout();
    Task<bool> Delete(string email);
}
