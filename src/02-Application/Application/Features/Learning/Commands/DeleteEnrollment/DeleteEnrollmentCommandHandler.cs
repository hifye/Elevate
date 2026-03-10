using Application.Abstraction.Persistance.Repositories.Learning;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Commands.DeleteEnrollment;

public class DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEnrollmentCommand, Result>
{
    public async Task<Result> Handle(DeleteEnrollmentCommand command, CancellationToken cancellationToken)
    {
        var deleted = await enrollmentRepository.Delete(command.UserId);
        if(!deleted)
            return Result.Failure("Enrollment not found", "Not Found");
        
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}