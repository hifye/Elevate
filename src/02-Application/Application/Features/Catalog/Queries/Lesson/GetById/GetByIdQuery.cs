using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Lesson;

public record GetByIdQuery(int Id) : IRequest<Result<Domain.Entities.Catalog.Lesson>>;
