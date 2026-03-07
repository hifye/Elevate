using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<Domain.Entities.Catalog.Course>>;