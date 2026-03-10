using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.DeleteCourse;

public class DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCourseCommand, Result>
{
    public async Task<Result> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
    {
        var deleted = await courseRepository.Delete(command.Id);
        if (!deleted)
            return Result.Failure("Course not found", "Not Found");
        
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}