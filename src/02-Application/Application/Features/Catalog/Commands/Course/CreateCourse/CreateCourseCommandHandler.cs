using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.CreateCourse;

public class CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCourseCommand, Result>
{
    public async Task<Result> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var result = Domain.Entities.Catalog.Course.Create(command.Title, command.Description, command.Price, command.InstructorId);
        
        if(result.IsFailure)
            return Result<CourseResponse>.Failure(result.Error!);
        
        await courseRepository.Create(result.Value!);
        await unitOfWork.CommitAsync();

        return Result.Success();
    }
}