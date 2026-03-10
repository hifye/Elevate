using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAll;

public record GetAllQuery : IRequest<Result<IEnumerable<CourseListItem>>>;