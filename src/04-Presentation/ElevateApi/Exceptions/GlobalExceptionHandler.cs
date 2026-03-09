using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Exceptions;

/// <summary>
/// Classe responsável por manipular exceções globais em uma aplicação ASP.NET Core.
/// </summary>
/// <remarks>
/// Esta classe implementa a interface <see cref="IExceptionHandler"/> e fornece
/// uma forma centralizada de capturar e tratar exceções que não foram antecipadas em outras áreas da aplicação.
/// Quando uma exceção é capturada, um objeto <see cref="ProblemDetails"/> é retornado ao cliente
/// com informações sobre o erro ocorrido.
/// </remarks>
public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problem = new ProblemDetails
        {
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://httpstatuses.com/500"
        };
        
        httpContext.Response.StatusCode = problem.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}