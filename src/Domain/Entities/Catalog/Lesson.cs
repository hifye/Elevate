using Domain.Commom;

namespace Domain.Entities.Catalog;

public class Lesson
{
    public int Id { get; private set; }
    public int ModuleId { get; private set; }
    public string Title { get; private set; }
    public string VideoUrl { get; private set; }
    public int OrderNumber { get; private set; }

    private Lesson(int moduleId, string title, string videoUrl, int orderNumber)
    {
        ModuleId = moduleId;
        Title = title;
        VideoUrl = videoUrl;
        OrderNumber = orderNumber;
    }

    public static Result<Lesson> Create(
        int moduleId,
        string title,
        string videoUrl,
        int orderNumber
    )
    {
        return Guard
            .AgainstOutOfRange(moduleId < 1, "Module Id must be greater than 0")
            .Bind(() => Guard.AgainstNullOrWhiteSpace(title, "Title cannot be null"))
            .Bind(() =>
                title.Length > 100
                    ? Result.Failure("Title cannot be longer than 100 characters.")
                    : Result.Success()
            )
            .Bind(() => Guard.AgainstNullOrWhiteSpace(videoUrl, "VideoUrl cannot be null"))
            .Bind(() =>
                videoUrl.Length > 300
                    ? Result.Failure("Video Url cannot be longer than 300 characters.")
                    : Result.Success()
            )
            .Bind(() =>
                Guard.AgainstOutOfRange(orderNumber < 1, "Order Number must be greater than 0")
            )
            .Map(() => new Lesson(moduleId, title, videoUrl, orderNumber));
    }
}
