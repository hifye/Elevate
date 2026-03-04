namespace Domain.Commom;

/// <summary>
/// Classe auxiliar para validar condições e criar resultados baseados nessas validações.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Verifica se a string fornecida é nula ou contém apenas espaços em branco.
    /// Retorna um resultado indicando falha com uma mensagem de erro ou sucesso.
    /// </summary>
    /// <param name="value">Valor de entrada que será verificado.</param>
    /// <param name="message">Mensagem de erro a ser retornada caso a validação falhe.</param>
    /// <returns>Um objeto <see cref="Result"/> representando o resultado da validação.</returns>
    public static Result AgainstNullOrWhiteSpace(string value, string message)
    {
        if (String.IsNullOrWhiteSpace(value))
            return Result.Failure(message);

        return Result.Success();
    }

    /// <summary>
    /// Verifica se uma condição indica que um valor está fora de um intervalo permitido.
    /// Retorna um resultado indicando falha com uma mensagem de erro ou sucesso.
    /// </summary>
    /// <param name="condition">Condição a ser avaliada. Caso seja verdadeira, a validação falhará.</param>
    /// <param name="message">Mensagem de erro a ser retornada caso a validação falhe.</param>
    /// <returns>Um objeto <see cref="Result"/> representando o resultado da validação.</returns>
    public static Result AgainstOutOfRange(bool condition, string message)
    {
        if (condition)
            return Result.Failure(message);

        return Result.Success();
    }
}
