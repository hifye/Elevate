using Application.Contracts.Repositories.Learning;
using Application.Contracts.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Commands.DeleteEnrollment;

public class DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEnrollmentCommand, Result>
{
    private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<Result> Handle(DeleteEnrollmentCommand command, CancellationToken cancellationToken)
    {
        _enrollmentRepository.Delete(command.UserId);
        _unitOfWork.CommitAsync();
        return Task.FromResult(Result.Success());
    }
}