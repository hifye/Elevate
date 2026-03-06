using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
using Application.Features.Catalog.Responses;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.CreateLesson;

public class CreateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateLessonCommand, Result<LessonResponse>>
{
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<LessonResponse>> Handle(CreateLessonCommand command, CancellationToken cancellationToken)
    {
        var result = Domain.Entities.Catalog.Lesson.Create(command.ModuleId, command.Title, command.VideoUrl,
                command.OrderNumber);
        if (result.IsFailure)
            return Result<LessonResponse>.Failure(result.Error!);

        await _lessonRepository.Create(result.Value!);
        await _unitOfWork.CommitAsync();
        return Result<LessonResponse>.Success(_mapper.Map<LessonResponse>(result.Value));
    }
}