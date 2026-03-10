using Application.Features.Catalog.ListItem;
using Application.Features.Catalog.Queries.Module.GetModulesAndLessons;

namespace Application.Abstraction.Queries;

public interface IModuleQueries
{
    Task<IEnumerable<ModuleListItem>> GetModulesAndLessons();
}