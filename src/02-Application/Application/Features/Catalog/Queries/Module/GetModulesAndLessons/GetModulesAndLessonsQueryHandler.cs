using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Module.GetModulesAndLessons;

public class GetModulesAndLessonsQueryHandler(IModuleRepository moduleRepository)
    : IRequestHandler<GetModulesAndLessonsQuery, Result<IEnumerable<ModuleResponse>>>
{
    public async Task<Result<IEnumerable<ModuleResponse>>> Handle(GetModulesAndLessonsQuery request, CancellationToken cancellationToken)
    {
        var modules = await moduleRepository.GetModulesAndLessons();
        if(modules == null || !modules.Any())
            return Result<IEnumerable<ModuleResponse>>.Failure("Modules not found", "Not Found");
        
        return Result<IEnumerable<ModuleResponse>>.Success(modules);
    }
}