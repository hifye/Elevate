using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.CreateLesson;

public class CreateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateLessonCommand, Result>
{
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Result> Handle(CreateLessonCommand command, CancellationToken cancellationToken)
    {
        var result = Domain.Entities.Catalog.Lesson.Create(command.ModuleId, command.Title, command.VideoUrl,
                command.OrderNumber);
        if (result.IsFailure)
            return Result.Failure(result.Error!);

        await _lessonRepository.Create(result.Value!);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}