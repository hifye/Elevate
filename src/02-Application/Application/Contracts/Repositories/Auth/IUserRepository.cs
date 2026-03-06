using Domain.Entities.Auth;
using Domain.ValuesObjects;

namespace Application.Contracts.Repositories.Auth;

public interface IUserRepository
{
    Task<User> GetUserByEmail(Email email);
    Task<User> GetUserById(Guid id);
    Task CreateUser(User user);
}