namespace Infrastructure.Persistance.Dapper.Queries.Auth;

public class AuthQueries
{
    public const string GetUserByEmail = """
                                         select id as Id,
                                         role_id as RoleId,
                                         name as Name,
                                         email as Email, 
                                         password_hash as PasswordHash,
                                         refresh_token as RefreshToken, 
                                         refresh_token_expires_at as RefreshTokenExpiresAt
                                         from auth.users
                                         where email = @Email
                                         """;

    public const string GetUserById = """
                                      select id as Id,
                                      role_id as RoleId,
                                      name as Name, 
                                      email as Email, 
                                      password_hash as PasswordHash,
                                      refresh_token as RefreshToken, 
                                      refresh_token_expires_at as RefreshTokenExpiresAt
                                      from auth.users
                                      where id = @Id
                                      order by Id
                                      """;

    public const string CreateUser = """
                                     insert into auth.users(
                                     role_id, 
                                     name, 
                                     email, 
                                     password_hash)
                                     values(
                                     @RoleId, 
                                     @Name,
                                     @Email,
                                     @PasswordHash)
                                     """;

    public const string UpdatePassword = """
                                         update auth.users
                                         set password_hash = @PasswordHash
                                         where id = @UserId
                                         """;
}