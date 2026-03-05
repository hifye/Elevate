using System.Data;
using System.Reflection;
using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
using Dapper;

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
                                            @"insert into catalog.modules(
	                                                    course_id,
	                                                    title,
	                                                    order_number)
                                                  values(
	                                                    @CourseId,
	                                                    @Title,
	                                                    @OrderNumber)", module, _unitOfWork.Transaction);

    public void Update(Module module) => _contextDapper.ExecuteAsync(
								    @"update catalog.modules
											set title as Title,
											order_number as OrderNumber
											where id = @Id", module, _unitOfWork.Transaction);

    public async Task<bool> Delete(int id)
    {
	    var rowsAffected = await _contextDapper.ExecuteAsync(
						       @"delete from catalog.modules
								   	   where id = @Id", new { Id = id }, _unitOfWork.Transaction);
	    return rowsAffected > 0;
    }
}