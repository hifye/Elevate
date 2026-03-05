using Application.Contracts.Responses.Catalog;
using Application.DTOs.Catalog;

namespace Application.Contracts.Services.Catalog;

public interface IModuleService
{
    Task<ModuleResponse> Create(ModuleRequest request);
    Task<bool> Update(ModuleRequest request, int id);
    Task<bool> Delete(int id);
}