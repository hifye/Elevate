using Domain.ValuesObjects;

namespace Application.Features.Catalog.Responses;

public record CourseResponse(Guid Id, string Title, string Description, Price Price, Guid InstructorId)
{
    public List<ModuleResponse> Modules { get; init; } = [];
}