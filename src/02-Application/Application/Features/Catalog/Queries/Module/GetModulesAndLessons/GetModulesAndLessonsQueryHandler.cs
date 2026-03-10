using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Module.GetModulesAndLessons;

public class GetModulesAndLessonsQueryHandler(IModuleQueries moduleQueries)
    : IRequestHandler<GetModulesAndLessonsQuery, Result<IEnumerable<ModuleListItem>>>
{
    public async Task<Result<IEnumerable<ModuleListItem>>> Handle(GetModulesAndLessonsQuery request, CancellationToken cancellationToken)
    {
        var modules = await moduleQueries.GetModulesAndLessons();
        if(modules == null || !modules.Any())
            return Result<IEnumerable<ModuleListItem>>.Failure("Modules not found", "Not Found");
        
        return Result<IEnumerable<ModuleListItem>>.Success(modules);
    }
}