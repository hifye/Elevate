using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.DeleteLesson;

public class DeleteLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteLessonCommand, Result>
{
    public async Task<Result> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.GetById(command.Id);
        if (lesson == null)
            return Result.Failure("Lesson not found", "Not Found");
        
        await lessonRepository.Delete(command.Id);
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}