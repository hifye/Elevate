using Module = Domain.Entities.Catalog.Module;

namespace Application.Abstraction.Persistance.Repositories.Catalog;

public interface IModuleRepository
{
    Task<Module> GetById(int id);
    Task Create(Module module);
    Task<bool> Update(Module module);
    Task<bool> Delete(int id);
}