namespace Domain.Commom;

/// <summary>
/// Representa o resultado de uma operação, podendo ser um sucesso ou uma falha.
/// </summary>
public class Result(bool isSuccess, string? error)
{
    private bool IsSuccess { get; } = isSuccess; 
    public bool IsFailure => !IsSuccess; 
    protected string? Error { get; } = error; 

    public static Result Success() => new Result(true, null);

    public static Result Failure(string error) => new(false, error);

    /// <summary>
    /// Encadeia a execução de outra função que retorna um <see cref="Result"/> ou <see cref="Result{T}"/>.
    /// Caso o estado atual seja uma falha, o método interrompe a execução e retorna o próprio erro.
    /// </summary>
    /// <param name="func">
    /// Função a ser executada se o estado atual for um sucesso. Deve retornar uma instância de <see cref="Result"/>.
    /// </param>
    /// <returns>
    /// Um novo <see cref="Result"/> retornado pela função passada, caso o estado atual seja um sucesso.
    /// Caso contrário, retorna o estado de falha atual.
    /// </returns>
    public Result Bind(Func<Result> func)
    {
        if (IsFailure)
            return this;
        return func(); 
    }

    /// <summary>
    /// Encadeia a execução de outra função que retorna um <see cref="Result"/> ou <see cref="Result{T}"/>.
    /// Caso o estado atual seja uma falha, o método interrompe a execução e retorna o próprio erro.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo genérico do valor retornado pela função fornecida.
    /// </typeparam>
    /// <param name="func">
    /// Função a ser executada se o estado atual for um sucesso. Deve retornar uma instância de <see cref="Result{T}"/>.
    /// </param>
    /// <returns>
    /// Um novo <see cref="Result{T}"/> retornado pela função passada, caso o estado atual seja um sucesso.
    /// Caso contrário, retorna o estado de falha atual.
    /// </returns>
    public Result<T> Bind<T>(Func<Result<T>> func)
    {
        if (IsFailure)
            return Result<T>.Failure(Error!);

        return func();
    }

    /// <summary>
    /// Converte o estado atual de um <see cref="Result"/> em um <see cref="Result{T}"/>, aplicando uma função ao estado de sucesso.
    /// Caso o estado atual seja uma falha, o método interrompe a execução e retorna o próprio erro.
    /// </summary>
    /// <param name="func">
    /// Função a ser executada para transformar o estado de sucesso do <see cref="Result"/> em um valor de tipo <typeparamref name="T"/>.
    /// </param>
    /// <typeparam name="T">
    /// O tipo do valor retornado pela função fornecida e encapsulado no <see cref="Result{T}"/>.
    /// </typeparam>
    /// <returns>
    /// Um novo <see cref="Result{T}"/> que contém o valor retornado pela função executada, caso o estado atual seja um sucesso.
    /// Caso o estado atual seja uma falha, retorna um estado de falha com o mesmo erro.
    /// </returns>
    public Result<T> Map<T>(Func<T> func)
    {
        if (IsFailure)
            return Result<T>.Failure(Error!);
        return Result<T>.Success(func());
    }

    /// <summary>
    /// Executa uma ação encapsulada em um bloco try-catch, retornando um <see cref="Result"/> que indica sucesso ou falha.
    /// </summary>
    /// <param name="action">
    /// Ação a ser executada. Caso a execução lance uma exceção, esta será capturada.
    /// </param>
    /// <param name="errorMessage">
    /// Mensagem de erro que será associada ao <see cref="Result"/> em caso de falha.
    /// </param>
    /// <returns>
    /// Retorna um <see cref="Result.Success"/> se a ação for executada sem exceções.
    /// Retorna um <see cref="Result.Failure"/> com a mensagem de erro fornecida, caso uma exceção seja lançada.
    /// </returns>
    public static Result Try(Action action, string errorMessage)
    {
        try { action(); return Success(); }
        catch { return Failure(errorMessage); }
    }
}

public class Result<T> : Result
{
    public T? Value { get; }
    
    private Result(bool isSuccess, string? error, T? value)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, null, value);

    public new static Result<T> Failure(string error) => new(false, error, default);

    /// <summary>
    /// Encadeia a execução de outra função que retorna um <see cref="Result"/>.
    /// Caso o estado atual seja uma falha, o método interrompe a execução e retorna o próprio erro.
    /// </summary>
    /// <param name="func">
    /// Função a ser executada se o estado atual for um sucesso. Deve retornar uma instância de <see cref="Result"/>.
    /// </param>
    /// <returns>
    /// Um novo <see cref="Result"/> retornado pela função passada, caso o estado atual seja um sucesso.
    /// Caso contrário, retorna o estado de falha atual.
    /// </returns>
    public Result<K> Bind<K>(Func<T, Result<K>> func)
    {
        if (IsFailure)
            return Result<K>.Failure(Error!);

        return func(Value!);
    }

    /// <summary>
    /// Transforma o valor contido no estado atual em um novo valor do mesmo estado sucessor, utilizando a função fornecida.
    /// Caso o estado atual seja uma falha, o método interrompe a execução e retorna o próprio erro.
    /// </summary>
    /// <param name="func">
    /// Função que recebe o valor associado ao estado de sucesso e o transforma em um novo valor.
    /// </param>
    /// <typeparam name="K">
    /// Tipo do novo valor gerado pela função.
    /// </typeparam>
    /// <returns>
    /// Um novo <see cref="Result{K}"/> com o valor gerado pela função caso o estado atual seja um sucesso.
    /// Caso contrário, retorna o estado de falha atual.
    /// </returns>
    public Result<K> Map<K>(Func<T, K> func)    
    {
        if (IsFailure)
            return Result<K>.Failure(Error!);

        return Result<K>.Success(func(Value!));
    }
}
