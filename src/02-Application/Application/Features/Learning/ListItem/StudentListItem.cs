namespace Application.Features.Learning.ListItem;

public record StudentListItem(Guid StudentId, string StudentName, string StudentEmail)
{
    public List<EnrollmentListItem> Enrollments { get; init; } = [];
}