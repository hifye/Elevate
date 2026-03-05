using System.Reflection;
using Module = Domain.Entities.Catalog.Module;

namespace Application.Contracts.Repositories.Catalog;

public interface IModuleRepository
{
    Task Create(Domain.Entities.Catalog.Module module);
    Task<bool> Update(Module module);
    Task<bool> Delete(int id);
}