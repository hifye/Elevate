using System.Data;
using Application.Contracts.Repositories.Auth;
using Application.Contracts.Responses.Auth;
using Application.Contracts.UnitOfWork;
using Dapper;
using Domain.Entities.Auth;
using Domain.ValuesObjects;

namespace Infrastructure.Repositories.Auth;

public class UserRepository : IUserRepository
{
    #region Dependencies

    private readonly IDbConnection _contextDapper;
    private readonly IUnitOfWork _unitOfWork;
    
        public UserRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork)
        {
	        _contextDapper = contextDapper;
	        _unitOfWork = unitOfWork;
        }

    #endregion

    public async Task<InstructorResponse> GetUserByEmail(Email email) => _contextDapper.QueryFirstOrDefault<InstructorResponse>(
                                                            @"select id as Id,
	                                                                     name as Name, 
	                                                                     email as Email, 
	                                                                     password_hash as PasswordHash,
																		 refresh_token as RefreshToken, 
																		 refresh_token_expires_at as RefreshTokenExpiresAt
                                                                 from auth.users
                                                                 where email = @Email", new { Email = email })!;
	
    public async Task<User> GetUserById(Guid id) => (await _contextDapper.QueryFirstOrDefaultAsync<User>(
											@"select id as Id,
	                                                     name as Name, 
	                                                     email as Email, 
	                                                     password_hash as PasswordHash,
	                                                     refresh_token as RefreshToken, 
														 refresh_token_expires_at as RefreshTokenExpiresAt
                                                  from auth.users
                                                  where id = @Id
                                                  order by Id", new {Id = id})!)!;

    public async Task CreateUser(User user) => await _contextDapper.ExecuteAsync(
										@"insert into auth.users(
                       									  role_id as RoleId, 
									                      name as Name, 
									                      email as Email, 
									                      password_hash as PasswordHash)
												  values(@RoleId, 
		   												 @Name,
		   												 @Email,
		   												 @PasswordHash)", user , _unitOfWork.Transaction);
}