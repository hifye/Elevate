using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.PatchCourse;

public class PatchCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork) : IRequestHandler<PatchCourseCommand, Result>
{
    public async Task<Result> Handle(PatchCourseCommand command, CancellationToken cancellationToken)
    {
        var patch = await courseRepository.GetById(command.Id);
        if(patch is null)
            return Result.Failure("Course not found", "Not Found");

        var result = patch.ApplyPatch(command.Title, command.Description, command.Price);
        if (result.IsFailure)
            return result;

        await courseRepository.Update(patch);       
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}