using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.UpdateLesson;

public class UpdateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateLessonCommand, Result>
{
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetById(command.Id);
        if (lesson == null)
            return Result.Failure("Lesson not found", "Not Found");

        var result = lesson.Update(command.Id, command.Title, command.VideoUrl, command.OrderNumber);
        if (result.IsFailure)
            return Result.Failure(result.Error!);
        
        await _lessonRepository.Update(lesson);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}