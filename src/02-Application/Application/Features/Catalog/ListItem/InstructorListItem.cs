using Domain.ValuesObjects;

namespace Application.Features.Catalog.ListItem;

public record InstructorListItem(Guid InstructorId, string InstructorName, Email InstructorEmail)
{
    public List<CourseListItem> Courses { get; init; } = [];
}