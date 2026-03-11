using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.CreateCourse;

public record CreateCourseCommand( 
    string Title, 
    string Description, 
    decimal Price) : IRequest<Result>;