using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetCoursesByTitle;

public abstract record GetCoursesByTitleQuery(string Title) : IRequest<Result<IEnumerable<CourseResponse>>>;