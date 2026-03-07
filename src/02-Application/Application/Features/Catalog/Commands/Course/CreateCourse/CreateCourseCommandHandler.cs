using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.CreateCourse;

public class CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCourseCommand, Result>
{
    /// <summary>
    /// Manipula o comando de criação de curso, gerenciando a lógica de validação, criação da entidade
    /// e persistência no repositório associado.
    /// </summary>
    /// <param name="command">Objeto que contém os dados necessários para criar um novo curso.</param>
    /// <param name="cancellationToken">Token usado para cancelar a operação de forma cooperativa.</param>
    /// <returns>Um objeto <see cref="Result{T}"/> contendo o resultado da operação,
    /// incluindo um <see cref="CourseResponse"/> em caso de sucesso ou uma mensagem de erro em caso de falha.</returns>
    public async Task<Result> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var result = Domain.Entities.Catalog.Course.Create(command.Title, command.Description, command.Price, command.InstructorId);
        
        if(result.IsFailure)
            return Result<CourseResponse>.Failure(result.Error!);
        
        await courseRepository.Create(result.Value!);
        await unitOfWork.CommitAsync();

        return Result.Success();
    }
}