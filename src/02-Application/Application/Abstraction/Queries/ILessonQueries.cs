using Application.Features.Catalog.ListItem;
using Application.Features.Catalog.Queries.Lesson.GetAllLessons;

namespace Application.Abstraction.Queries;

public interface ILessonQueries
{
    Task<IEnumerable<LessonListItem>> GetAllLessons();
}