using Application.Contracts.UnitOfWork;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.DeleteCourse;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
{
    #region Dependencies
    
    private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
    
        public DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }
    
    #endregion
    
    public async Task<Result> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
    {
        await _courseRepository.Delete(command.Id);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}