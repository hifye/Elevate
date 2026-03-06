using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
using Application.Features.Catalog.Responses;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.UpdateLesson;

public class UpdateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateLessonCommand, Result<LessonResponse>>
{
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<LessonResponse>> Handle(UpdateLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetById(command.Id);
        if (lesson == null)
            return Result<LessonResponse>.Failure("Lesson not found");

        var result = lesson.Update(command.Id, command.Title, command.VideoUrl, command.OrderNumber);
        if (result.IsFailure)
            return Result<LessonResponse>.Failure(result.Error!);
        
        await _lessonRepository.Update(lesson);
        await _unitOfWork.CommitAsync();
        return Result<LessonResponse>.Success(_mapper.Map<LessonResponse>(lesson));
    }
}