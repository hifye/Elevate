using System.Data;
using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
using Dapper;
using Module = Domain.Entities.Catalog.Module;

namespace Infrastructure.Repositories.Catalog;

public class ModuleRepository : IModuleRepository
{
    #region Dependencies
    
    private readonly IDbConnection _contextDapper;
    private readonly IUnitOfWork _unitOfWork;

    public ModuleRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork)
    {
	    _contextDapper = contextDapper;
	    _unitOfWork = unitOfWork;
    }

    #endregion
    
    public async Task Create(Module module) => await _contextDapper.ExecuteAsync(
                                            @"insert into catalog.modules(course_id, title, order_number)
                                                  values(@CourseId, @Title, @OrderNumber)",
                                            new
                                            {
	                                            module.CourseId, 
	                                            module.Title, 
	                                            module.OrderNumber
                                            });

    public async Task<bool> Update(Module module)
    {
	   var rowsAffected = await _contextDapper.ExecuteAsync(
										@"update catalog.modules
											set title = @Title,
											order_number = @OrderNumber,
											where id = @Id",
										new
										{
											module.Title, 
											module.OrderNumber, 
											module.Id
										}, _unitOfWork.Transaction);
	   return rowsAffected > 0;

    }
    public async Task<bool> Delete(int id)
    {
	    var rowsAffected = await _contextDapper.ExecuteAsync(
						       @"delete from catalog.modules
								   	   where id = @Id", new { Id = id }, _unitOfWork.Transaction);
	    return rowsAffected > 0;
    }
}