using Domain.Entity;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
}
