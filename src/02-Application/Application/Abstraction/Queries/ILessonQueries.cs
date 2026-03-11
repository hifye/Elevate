using Application.Features.Catalog.ListItem;

namespace Application.Abstraction.Queries;

public interface ILessonQueries
{
    Task<IEnumerable<LessonListItem>> GetAllLessons();
}