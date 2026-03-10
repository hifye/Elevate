using System.Data;
using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Infrastructure.Data.Queries.Module;
using Module = Domain.Entities.Catalog.Module;

namespace Infrastructure.Persistance.Repositories.Catalog;

public class ModuleRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : IModuleRepository
{
	public Task<IEnumerable<ModuleResponse>> GetModulesAndLessons()
		=> contextDapper.QueryAsync<ModuleResponse>(ModuleQueries.GetModulesAndLessons);

	public async Task<Module> GetById(int id)
		=> (await contextDapper.QueryFirstOrDefaultAsync<Module>(ModuleQueries.GetById, new { Id = id }))!;
	
    public async Task Create(Module module) 
	    => await contextDapper.ExecuteAsync(ModuleQueries.Create,new { module.CourseId, module.Title, module.OrderNumber });
    
    public async Task<bool> Update(Module module)
    {
	   var rowsAffected = 
		   await contextDapper.ExecuteAsync(
			   ModuleQueries.Update, new { module.Title, module.OrderNumber, module.Id }, unitOfWork.Transaction);
	   return rowsAffected > 0;
    }
    
    public async Task<bool> Delete(int id)
    {
	    var rowsAffected = await contextDapper.ExecuteAsync(ModuleQueries.Delete, new { Id = id }, unitOfWork.Transaction);
	    return rowsAffected > 0;
    }
}