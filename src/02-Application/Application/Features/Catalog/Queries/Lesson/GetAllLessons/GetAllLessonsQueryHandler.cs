using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Lesson.GetAllLessons;

public class GetAllLessonsQueryHandler(ILessonQueries lessonQueries)
    : IRequestHandler<GetAllLessonsQuery, Result<IEnumerable<LessonListItem>>>
{
    public async Task<Result<IEnumerable<LessonListItem>>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
    {
        var lessons = await lessonQueries.GetAllLessons();
        if(lessons == null || !lessons.Any())
            return Result<IEnumerable<LessonListItem>>.Failure("Lessons Not Found", "Not Found");
        
        return Result<IEnumerable<LessonListItem>>.Success(lessons);
    }
}