using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.DeleteLesson;

public class DeleteLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteLessonCommand, Result>
{
    public async Task<Result> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
    {
        var deleted = await lessonRepository.Delete(command.Id);
        if (!deleted)
            return Result.Failure("Lesson not found", "Not Found");
        
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}