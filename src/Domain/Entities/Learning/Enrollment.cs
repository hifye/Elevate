using Domain.Commom;

namespace Domain.Entities.Learning;

public class Enrollment
{
    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime EnrolledAt { get; private set; }

    private Enrollment(Guid userId, Guid courseId, DateTime enrolledAt)
    {
        UserId = userId;
        CourseId = courseId;
        EnrolledAt = enrolledAt;
    }

    public static Result<Enrollment> Create(Guid userId, Guid courseId, DateTime enrolledAt)
    {
        return Guard.AgainstOutOfRange(userId == Guid.Empty, "User id is required")
            .Bind(() => Guard.AgainstOutOfRange(courseId == Guid.Empty, "Course id must be greater than 0"))
            .Bind(() => Guard.AgainstOutOfRange(enrolledAt > DateTime.UtcNow, "Invalid Date"))
            .Map(() => new Enrollment(userId, courseId, enrolledAt));
    }
}
