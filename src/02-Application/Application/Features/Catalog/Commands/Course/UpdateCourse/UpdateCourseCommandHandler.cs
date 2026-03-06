using Application.Contracts.UnitOfWork;
using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<CourseResponse>>
{
    #region Dependencies

    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #endregion

    
    public async Task<Result<CourseResponse>> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetById(command.Id);
        if (course == null)
            return Result<CourseResponse>.Failure("Course not Found");

        var result = course.Update(command.Id, command.Title, command.Description, command.Price);
        if (result.IsFailure)
            return Result<CourseResponse>.Failure(result.Error!);

        await _courseRepository.Update(course);
        await _unitOfWork.CommitAsync();


        return Result<CourseResponse>.Success(_mapper.Map<CourseResponse>(course));
    }
}