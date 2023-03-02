using DTO;

namespace Domain.Interfaces;

public interface IUserService
{
    Task<bool> Register(RegisterDTO newuser);
    Task Login(LoginDTO loguser);
    Task Logout();
    Task Delete(string email);
}
