using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.DeleteCourse;

public class DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCourseCommand, Result>
{
    public async Task<Result> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetById(command.Id);
        if (course == null)
            return Result.Failure("Course not found", "Not Found");
        
        await courseRepository.Delete(command.Id);
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}