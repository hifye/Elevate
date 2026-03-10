using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.UpdateLesson;

public class UpdateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateLessonCommand, Result>
{
    public async Task<Result> Handle(UpdateLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.GetById(command.Id);
        if (lesson == null)
            return Result.Failure("Lesson not found", "Not Found");

        var result = lesson.Update(command.Id, command.Title, command.VideoUrl, command.OrderNumber);
        if (result.IsFailure)
            return Result.Failure(result.Error!);
        
        await lessonRepository.Update(lesson);
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}