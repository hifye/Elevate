using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Lesson.GetAllLessons;

public class GetAllLessonsQueryHandler(ILessonRepository lessonRepository)
    : IRequestHandler<GetAllLessonsQuery, Result<IEnumerable<LessonResponse>>>
{
    public async Task<Result<IEnumerable<LessonResponse>>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
    {
        var lessons = await lessonRepository.GetAllLessons();
        if(lessons == null || !lessons.Any())
            return Result<IEnumerable<LessonResponse>>.Failure("Lessons Not Found", "Not Found");
        
        return Result<IEnumerable<LessonResponse>>.Success(lessons);
    }
}