namespace Application.Features.Learning.ListItem;

public record StudentSqlRow(Guid StudentId, string StudentName, string StudentEmail, string? Courses);