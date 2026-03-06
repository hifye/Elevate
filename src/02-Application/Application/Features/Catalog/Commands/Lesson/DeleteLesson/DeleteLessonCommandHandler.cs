using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
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
        await _lessonRepository.Delete(command.Id);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}