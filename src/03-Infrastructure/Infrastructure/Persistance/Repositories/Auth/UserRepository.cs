using System.Data;
using Application.Abstraction.Persistance.Repositories.Auth;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Auth;
using Domain.ValuesObjects;
using Infrastructure.Data.Sql;

namespace Infrastructure.Persistance.Repositories.Auth;

public class UserRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : IUserRepository
{
    
    public async Task<User> GetUserByEmail(Email email) 
	    => (await contextDapper.QueryFirstOrDefaultAsync<User>(AuthSql.GetUserByEmail, new { Email = email }))!;
	
    public async Task<User> GetUserById(Guid id) 
	    => (await contextDapper.QueryFirstOrDefaultAsync<User>(AuthSql.GetUserById, new {Id = id}))!;

    public async Task CreateUser(User user) 
	    => await contextDapper.ExecuteAsync(
		    AuthSql.CreateUser, new { user.RoleId, user.Name, user.Email, user.PasswordHash }, unitOfWork.Transaction);

    public async Task UpdatePassword(Guid userId, string newHash)
	    => await contextDapper.ExecuteAsync(AuthSql.UpdatePassword,
		    new { UserId = userId, PasswordHash = newHash });

    public Task UpdateRefreshToken(User user)
		=> contextDapper.ExecuteAsync(AuthSql.UpdateRefreshToken, user, unitOfWork.Transaction);

    public async Task<bool> Logout(Guid id)
    {
	    var rowsAffected = 
		    await contextDapper.ExecuteAsync(AuthSql.Logout, new { Id = id }, unitOfWork.Transaction);
	    return rowsAffected > 0;
    }
}