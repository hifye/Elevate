using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Manipula a execução de uma solicitação dentro de um pipeline, registrando informações de log antes, durante e após o processamento.
    /// </summary>
    /// <param name="request">
    /// O objeto da solicitação que está sendo processada.
    /// </param>
    /// <param name="next">
    /// Um delegate que representa a próxima etapa no pipeline a ser executada.
    /// </param>
    /// <param name="cancellationToken">
    /// Um token que pode ser usado para cancelar a operação assíncrona.
    /// </param>
    /// <returns>
    /// O resultado da execução da etapa do pipeline, representado como um objeto do tipo TResponse.
    /// </returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogInformation("Handling {RequestName} {@Request}", requestName, request);

        var stopwatch = Stopwatch.StartNew();

        try
        {
            var response = await next();

            stopwatch.Stop();

            logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms", requestName,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            logger.LogError(ex, "Error Handling {RequestName} after {ElapsedMilliseconds}ms", requestName,
                stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}