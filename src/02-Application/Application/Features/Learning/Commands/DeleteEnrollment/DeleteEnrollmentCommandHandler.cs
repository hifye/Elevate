using Application.Interfaces.Repositories.Learning;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Commands.DeleteEnrollment;

public class DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEnrollmentCommand, Result>
{
    public Task<Result> Handle(DeleteEnrollmentCommand command, CancellationToken cancellationToken)
    {
        enrollmentRepository.Delete(command.UserId);
        unitOfWork.CommitAsync();
        return Task.FromResult(Result.Success());
    }
}