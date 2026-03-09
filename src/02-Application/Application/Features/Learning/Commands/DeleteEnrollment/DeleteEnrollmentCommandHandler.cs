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
        var result = enrollmentRepository.GetById(command.UserId);
        if (result == null)
            return Task.FromResult(Result.Failure("Enrollment not found", "Not Found"));       
        
        enrollmentRepository.Delete(command.UserId);
        unitOfWork.CommitAsync();
        return Task.FromResult(Result.Success());
    }
}