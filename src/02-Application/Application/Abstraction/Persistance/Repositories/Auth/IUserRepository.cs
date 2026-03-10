using Domain.Entities.Auth;
using Domain.ValuesObjects;

namespace Application.Abstraction.Persistance.Repositories.Auth;

public interface IUserRepository
{
    Task<User> GetUserByEmail(Email email);
    Task<User> GetUserById(Guid id);
    Task CreateUser(User user);
    Task UpdatePassword(Guid id, string newHash);
    Task UpdateRefreshToken(User user);
    Task<bool> Logout(Guid id);
}