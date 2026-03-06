using Domain.Commom;
using Domain.ValuesObjects;

namespace Domain.Entities.Auth;

public class User
{
    public Guid Id { get; private set; }
    public int RoleId { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }

    private User(int roleId, string name, Email email, string passwordHash)
    {
        RoleId = roleId;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static Result<User> Create(int roleId, string name, string email, string passwordHash)
    {
        return Guard
            .AgainstOutOfRange(roleId < 1, "Role id must be greater than 0.")
            .Bind(() => Guard.AgainstNullOrWhiteSpace(name, "Name cannot be null or empty."))
            .Bind(() =>
                name.Length > 100
                    ? Result.Failure("Name cannot be longer than 100 characters.")
                    : Result.Success()
            )
            .Bind(() =>
                Guard.AgainstNullOrWhiteSpace(passwordHash, "Password cannot be null or empty.")
            )
            .Bind(() => Email.Create(email))
            .Map(validEmail => new User(roleId, name, validEmail, passwordHash));
    }
}
