using Domain.Commom;
using Domain.ValuesObjects;

namespace Domain.Entities.Catalog;

public class Course
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public Guid InstructorId { get; private set; }

    private Course(string title, string description, Price price, Guid instructorId)
    {
        Title = title;
        Description = description;
        Price = price;
        InstructorId = instructorId;
    }

    public static Result<Course> Create(
        string title,
        string description,
        decimal price,
        Guid instructorId
    )
    {
        return Guard
            .AgainstNullOrWhiteSpace(title, "Title cannot be null")
            .Bind(() => title.Length > 100 ? Result.Failure("Title cannot be longer than 100 characters.") : Result.Success())
            .Bind(() => Guard.AgainstNullOrWhiteSpace(description, "Description cannot be null"))
            .Bind(() => Guard.AgainstOutOfRange(price < 1, "Price must be greater than 0"))
            .Bind(() =>
                Guard.AgainstOutOfRange(instructorId == Guid.Empty, "Instructor id is required")
            )
            .Bind(() => Price.Create(price))
            .Map(validPrice => new Course(title, description, validPrice, instructorId));
    }
}
