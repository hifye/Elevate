using Application.Contracts.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Lesson;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Domain.Entities.Catalog.Lesson>
{
    private readonly ILessonRepository _lessonRepository;

    public GetByIdQueryHandler(ILessonRepository lessonRepository)
        => _lessonRepository = lessonRepository;
    

    public Task<Domain.Entities.Catalog.Lesson> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        => _lessonRepository.GetById(query.Id);
        
    
}