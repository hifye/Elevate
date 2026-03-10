using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.UpdateCourse;

public class UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCourseCommand, Result>
{
    public async Task<Result> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetById(command.Id);
        if (course == null)
            return Result.Failure("Course not Found", "Not Found");

        var result = course.Update(command.Id, command.Title, command.Description, command.Price);
        if (result.IsFailure)
            return Result.Failure(result.Error!);

        await courseRepository.Update(course);
        await unitOfWork.CommitAsync();


        return Result.Success();
    }
}