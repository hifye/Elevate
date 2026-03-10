using Application.Abstraction.Persistance.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Lesson;

public class GetByIdQueryHandler(ILessonRepository lessonRepository)
    : IRequestHandler<GetByIdQuery, Result<Domain.Entities.Catalog.Lesson>>
{
    public async Task<Result<Domain.Entities.Catalog.Lesson>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.GetById(query.Id);
        if (lesson == null || query.Id != lesson.Id)
            return Result<Domain.Entities.Catalog.Lesson>.Failure("Lesson not found", "Not Found");
        
        return Result<Domain.Entities.Catalog.Lesson>.Success(lesson);
    }
        
    
}