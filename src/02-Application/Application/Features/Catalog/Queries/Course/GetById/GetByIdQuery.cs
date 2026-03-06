using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Domain.Entities.Catalog.Course>;