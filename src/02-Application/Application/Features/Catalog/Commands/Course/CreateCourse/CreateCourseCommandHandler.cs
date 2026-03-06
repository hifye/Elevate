using Application.Contracts.UnitOfWork;
using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.CreateCourse;

public class CreateCourseCommandHandler(ICourseRepository _courseRepository, IUnitOfWork _unitOfWork, IMapper _mapper)
    : IRequestHandler<CreateCourseCommand, Result<CourseResponse>>
{
    /// <summary>
    /// Manipula o comando de criação de curso, gerenciando a lógica de validação, criação da entidade
    /// e persistência no repositório associado.
    /// </summary>
    /// <param name="command">Objeto que contém os dados necessários para criar um novo curso.</param>
    /// <param name="cancellationToken">Token usado para cancelar a operação de forma cooperativa.</param>
    /// <returns>Um objeto <see cref="Result{T}"/> contendo o resultado da operação,
    /// incluindo um <see cref="CourseResponse"/> em caso de sucesso ou uma mensagem de erro em caso de falha.</returns>
    public async Task<Result<CourseResponse>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var result = Domain.Entities.Catalog.Course.Create(command.Title, command.Description, command.Price, command.InstructorId);
        
        if(result.IsFailure)
            return Result<CourseResponse>.Failure(result.Error!);
        
        await _courseRepository.Create(result.Value!);
        await _unitOfWork.CommitAsync();

        return Result<CourseResponse>.Success(_mapper.Map<CourseResponse>(result.Value));
    }
}