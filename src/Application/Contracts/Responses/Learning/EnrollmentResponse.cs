namespace Application.Contracts.Responses.Learning;

public record EnrollmentResponse(Guid UserId, int CourseId, DateTime EnrolledAt);
