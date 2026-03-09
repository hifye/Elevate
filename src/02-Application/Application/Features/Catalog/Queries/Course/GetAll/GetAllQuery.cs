using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAll;

public record GetAllQuery : IRequest<Result<IEnumerable<CourseResponse>>>;