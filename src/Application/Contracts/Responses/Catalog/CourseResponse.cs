using Domain.ValuesObjects;

namespace Application.Contracts.Responses.Catalog;

public record CourseResponse(Guid Id, string Title, string Description, Price Price, Guid InstructorId)
{
    public List<ModuleResponse> Modules { get; init; } = [];
}