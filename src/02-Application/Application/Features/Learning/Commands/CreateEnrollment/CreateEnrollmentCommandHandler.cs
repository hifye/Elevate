using Application.Contracts.UnitOfWork;
using Application.Features.Learning.Responses;
using Application.Interfaces.Repositories.Learning;
using AutoMapper;
using Domain.Commom;
using Domain.Entities.Learning;
using MediatR;

namespace Application.Features.Learning.Commands.CreateEnrollment;

public class CreateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateEnrollmentCommand, Result<EnrollmentResponse>>
{
    private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<EnrollmentResponse>> Handle(CreateEnrollmentCommand command, CancellationToken cancellationToken)
    {
        var result = Enrollment.Create(command.UserId, command.CourseId, command.EnrolledAt);
        if (result.IsFailure)
            return Result<EnrollmentResponse>.Failure(result.Error!);

        await _enrollmentRepository.Create(result.Value!);
        await _unitOfWork.CommitAsync();
        return Result<EnrollmentResponse>.Success(_mapper.Map<EnrollmentResponse>(result.Value));
    }
}