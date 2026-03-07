using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetWithModulesAndLessons;

public record GetWithModulesAndLessonsQuery(Guid Id) : IRequest<Result<CourseResponse>>;