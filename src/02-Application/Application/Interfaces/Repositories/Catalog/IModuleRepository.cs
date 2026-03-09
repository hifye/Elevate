using Application.Features.Catalog.Responses;
using Module = Domain.Entities.Catalog.Module;

namespace Application.Interfaces.Repositories.Catalog;

public interface IModuleRepository
{
    Task<IEnumerable<ModuleResponse>> GetModulesAndLessons();
    Task<Module> GetById(int id);
    Task Create(Module module);
    Task<bool> Update(Module module);
    Task<bool> Delete(int id);
}