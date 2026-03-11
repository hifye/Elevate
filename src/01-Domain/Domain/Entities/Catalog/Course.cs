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
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Price = price;
        InstructorId = instructorId;
    }

    protected Course() { }

    public Result Update(Guid id, string title, string description, decimal price)
    {
        return Guard
            .AgainstOutOfRange(id == Guid.Empty, "Course id is required")
            .Bind(() => Guard.AgainstNullOrWhiteSpace(title, "Title cannot be null"))
            .Bind(() =>
                title.Length > 100
                    ? Result.Failure("Title cannot be longer than 100 characters.")
                    : Result.Success()
            )
            .Bind(() => Guard.AgainstNullOrWhiteSpace(description, "Description cannot be null"))
            .Bind(() => Price.Create(price))
            .Map(validPrice =>
            {
                Title = title;
                Description = description;
                Price = validPrice;

                return Result.Success();
            });
    }

    public Result ApplyPatch(string? title, string? description, decimal? price)
    {
        if (title is not null)
        {
            var result = UpdateTitle(title);
            if (result.IsFailure)
                return result;
        }
        
        if (description is not null)
        {
            var result = UpdateDescription(description);
            if (result.IsFailure)
                return result;
        }
        
        if (price is not null)
        {
            var result = UpdatePrice(price.Value);
            if (result.IsFailure)
                return result;
        }
        
        return Result.Success();       
    }

    public Result UpdateTitle(string title)
    {
        Guard
            .AgainstNullOrWhiteSpace(title, "Title cannot be null")
            .Bind(() =>
                title.Length > 100
                    ? Result.Failure("Title cannot be longer than 100 characters.")
                    : Result.Success()
            );

        Title = title;
        return Result.Success();
    }

    public Result UpdateDescription(string description)
    {
        Guard.AgainstNullOrWhiteSpace(description, "Description cannot be null");
        Description = description;
        return Result.Success();
    }

    public Result UpdatePrice(decimal price)
    {
        var result = Price.Create(price);
        if (result.IsFailure)
            return result;

        Price = result.Value!;
        return Result.Success();
    }

    public static Result<Course> Create(string title, string description, decimal price, Guid instructorId)
    {
        return Guard.AgainstNullOrWhiteSpace(title, "Title cannot be null")
            .Bind(() =>
                title.Length > 100
                    ? Result.Failure("Title cannot be longer than 100 characters.")
                    : Result.Success()
            )
            .Bind(() => Guard.AgainstNullOrWhiteSpace(description, "Description cannot be null"))
            .Bind(() => Guard.AgainstOutOfRange(price < 1, "Price must be greater than 0"))
            .Bind(() => Price.Create(price))
            .Map(validPrice => new Course(title, description, validPrice, instructorId));
    }
}
