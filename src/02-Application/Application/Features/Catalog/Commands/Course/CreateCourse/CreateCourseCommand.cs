using Domain.Commom;
using MediatR;
using CourseResponse = Application.Features.Catalog.Responses.CourseResponse;

namespace Application.Features.Catalog.Commands.Course.CreateCourse;

public record CreateCourseCommand( 
    string Title, 
    string Description, 
    decimal Price, 
    Guid InstructorId) : IRequest<Result<CourseResponse>>;