using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Exceptions;

/// <summary>
/// Classe responsável por lidar com exceções do tipo <see cref="FluentValidation.ValidationException"/>
/// dentro de aplicações que utilizam a biblioteca FluentValidation.
/// </summary>
/// <remarks>
/// Essa classe implementa <see cref="IExceptionHandler"/> e se encarrega
/// de capturar e manipular exceções de validação, formatando-as de maneira legível
/// e retornando uma resposta detalhada para o cliente.
/// </remarks>
public class ValidationExceptionHandler : IExceptionHandler
{
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
            return false;

        var errors = validationException.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );

        var problem = new ValidationProblemDetails(errors)
        {
            Title = "Validation Error",
            Status = StatusCodes.Status400BadRequest,
            Type = "https://httpstatuses.com/400"
        };
        
        httpContext.Response.StatusCode = problem.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        
        return true;
    }
}