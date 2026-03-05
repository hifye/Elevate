using System.Reflection;

namespace Application.Contracts.Repositories.Catalog;

public interface IModuleRepository
{
    Task Create(Module module);
    void Update(Module module);
    Task<bool> Delete(int id);
}