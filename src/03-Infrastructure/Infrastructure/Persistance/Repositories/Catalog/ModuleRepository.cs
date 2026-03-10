using System.Data;
using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Infrastructure.Data.Sql;
using Module = Domain.Entities.Catalog.Module;

namespace Infrastructure.Persistance.Repositories.Catalog;

public class ModuleRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : IModuleRepository
{
	public async Task<Module> GetById(int id)
		=> (await contextDapper.QueryFirstOrDefaultAsync<Module>(ModuleSql.GetById, new { Id = id }))!;
	
    public async Task Create(Module module) 
	    => await contextDapper.ExecuteAsync(ModuleSql.Create,new { module.CourseId, module.Title, module.OrderNumber });
    
    public async Task<bool> Update(Module module)
    {
	   var rowsAffected = 
		   await contextDapper.ExecuteAsync(
			   ModuleSql.Update, new { module.Title, module.OrderNumber, module.Id }, unitOfWork.Transaction);
	   return rowsAffected > 0;
    }
    
    public async Task<bool> Delete(int id)
    {
	    var rowsAffected = await contextDapper.ExecuteAsync(ModuleSql.Delete, new { Id = id }, unitOfWork.Transaction);
	    return rowsAffected > 0;
    }
}