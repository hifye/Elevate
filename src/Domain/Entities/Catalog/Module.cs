using Domain.Commom;

namespace Domain.Entities.Catalog;

public class Module
{
    public int Id { get; private set; }
    public Guid CourseId { get; private set; }
    public string Title { get; private set; }
    public int OrderNumber { get; private set; }

    private Module(Guid courseId, string title, int orderNumber)
    {
        CourseId = courseId;
        Title = title;
        OrderNumber = orderNumber;
    }

    public static Result<Module> Create(Guid courseId, string title, int orderNumber)
    {
        return Guard
            .AgainstOutOfRange(courseId == Guid.Empty, "Course Id is required")
            .Bind(() => Guard.AgainstNullOrWhiteSpace(title, "Title cannot be null"))
            .Bind(() =>
                title.Length > 100
                    ? Result.Failure("Title cannot be longer than 100 characters.")
                    : Result.Success()
            )
            .Bind(() =>
                Guard.AgainstOutOfRange(orderNumber < 1, "Order Number must be greater than 0")
            )
            .Map(() => new Module(courseId, title, orderNumber));
    }
}
