using Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Lesson;

public class GetByIdQueryHandler(ILessonRepository lessonRepository)
    : IRequestHandler<GetByIdQuery, Domain.Entities.Catalog.Lesson>
{
    public Task<Domain.Entities.Catalog.Lesson> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        => lessonRepository.GetById(query.Id);
        
    
}