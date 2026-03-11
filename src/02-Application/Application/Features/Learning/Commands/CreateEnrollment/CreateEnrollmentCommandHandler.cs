using Application.Abstraction.Persistance.Repositories.Learning;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using Domain.Entities.Learning;
using MediatR;

namespace Application.Features.Learning.Commands.CreateEnrollment;

public class CreateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    : IRequestHandler<CreateEnrollmentCommand, Result>
{
    public async Task<Result> Handle(CreateEnrollmentCommand command, CancellationToken cancellationToken)
    {
        var result = Enrollment.Create(currentUser.UserId, command.CourseId, command.EnrolledAt);
        if (result.IsFailure)
            return Result.Failure(result.Error!);

        await enrollmentRepository.Create(result.Value!);
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}