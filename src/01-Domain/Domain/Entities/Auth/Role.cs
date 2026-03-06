using Domain.Commom;

namespace Domain.Entities.Auth;

public class Role
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    private Role(string name)
    {
        Name = name;
    }

    public static Result<Role> Create(string name)
    {
        return Guard.AgainstNullOrWhiteSpace(name, "Name cannot be null")
            .Map(() => new Role(name));
    }
}
