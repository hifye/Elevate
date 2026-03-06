using FluentValidation;
using MediatR;

namespace Application.Behaviors;

/// <summary>
/// Classe que implementa um comportamento de validação no pipeline do MediatR.
/// Este comportamento utiliza validações definidas com FluentValidation para garantir que os objetos de requisição estejam consistentes antes de serem processados.
/// </summary>
/// <typeparam name="TRequest">O tipo da requisição sendo processada.</typeparam>
/// <typeparam name="TResponse">O tipo da resposta do manipulador associado à requisição.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;


    /// <summary>
    /// Método responsável por executar o comportamento de validação no pipeline do MediatR.
    /// Valida o objeto de requisição com base nos validadores configurados, lançando uma exceção caso sejam encontradas falhas de validação.
    /// </summary>
    /// <param name="request">O objeto de requisição que será validado.</param>
    /// <param name="next">O próximo delegado no pipeline, responsável por executar o próximo comportamento ou manipulador.</param>
    /// <param name="cancellationToken">Um token utilizado para propagar notificações de cancelamento.</param>
    /// <returns>Retorna o resultado processado pelo próximo comportamento ou manipulador no pipeline.</returns>
    /// <exception cref="ValidationException">Lançada quando são encontradas falhas de validação no objeto de requisição.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = results
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();
            if(failures.Count != 0)
                throw new ValidationException(failures);
        }
        return await next(cancellationToken);
    }
}