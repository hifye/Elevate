using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.DeleteLesson;

public class DeleteLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteLessonCommand, Result>
{
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetById(command.Id);
        if (lesson == null)
            return Result.Failure("Lesson not found", "Not Found");
        
        await _lessonRepository.Delete(command.Id);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}