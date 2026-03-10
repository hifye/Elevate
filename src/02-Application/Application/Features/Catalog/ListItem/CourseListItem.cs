namespace Application.Features.Catalog.ListItem;

public record CourseListItem(
    Guid CourseId,
    string CourseTitle,
    string CourseDescription,
    decimal CoursePrice,
    Guid InstructorId)
{
    public List<ModuleListItem> Modules { get; init; } = [];
}