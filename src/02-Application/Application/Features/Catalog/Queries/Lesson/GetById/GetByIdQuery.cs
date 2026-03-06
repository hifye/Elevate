using MediatR;

namespace Application.Features.Catalog.Queries.Lesson;

public record GetByIdQuery(int Id) : IRequest<Domain.Entities.Catalog.Lesson>;
