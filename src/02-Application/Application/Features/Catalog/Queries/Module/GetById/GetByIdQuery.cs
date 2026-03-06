using MediatR;

namespace Application.Features.Catalog.Queries.Module.GetById;

public record GetByIdQuery(int Id) : IRequest<Domain.Entities.Catalog.Module>;