using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAllWithModulesAndLessons;

public class GetAllWithModulesAndLessonsQuery : IRequest<Result<IEnumerable<CourseResponse>>>;