using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Module.GetById;

public record GetByIdQuery(int Id) : IRequest<Result<Domain.Entities.Catalog.Module>>;