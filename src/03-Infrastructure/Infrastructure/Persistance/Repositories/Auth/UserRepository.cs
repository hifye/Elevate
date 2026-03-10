using System.Data;
using Application.Interfaces.Repositories.Auth;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Auth;
using Domain.ValuesObjects;
using Infrastructure.Data.Queries.Auth;

namespace Infrastructure.Persistance.Repositories.Auth;

public class UserRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : IUserRepository
{
    
    public async Task<User> GetUserByEmail(Email email) 
	    => (await contextDapper.QueryFirstOrDefaultAsync<User>(AuthQueries.GetUserByEmail, new { Email = email }))!;
	
    public async Task<User> GetUserById(Guid id) 
	    => (await contextDapper.QueryFirstOrDefaultAsync<User>(AuthQueries.GetUserById, new {Id = id}))!;

    public async Task CreateUser(User user) 
	    => await contextDapper.ExecuteAsync(
		    AuthQueries.CreateUser, new { user.RoleId, user.Name, user.Email, user.PasswordHash }, unitOfWork.Transaction);

    public async Task UpdatePassword(Guid userId, string newHash)
	    => await contextDapper.ExecuteAsync(AuthQueries.UpdatePassword,
		    new { UserId = userId, PasswordHash = newHash });

    public Task UpdateRefreshToken(User user)
		=> contextDapper.ExecuteAsync(AuthQueries.UpdateRefreshToken, user, unitOfWork.Transaction);

    public async Task<bool> Logout(Guid id)
    {
	    var rowsAffected = 
		    await contextDapper.ExecuteAsync(AuthQueries.Logout, new { Id = id }, unitOfWork.Transaction);
	    return rowsAffected > 0;
    }
}