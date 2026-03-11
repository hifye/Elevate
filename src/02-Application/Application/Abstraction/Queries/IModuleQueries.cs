using Application.Features.Catalog.ListItem;

namespace Application.Abstraction.Queries;

public interface IModuleQueries
{
    Task<IEnumerable<ModuleListItem>> GetModulesAndLessons();
}