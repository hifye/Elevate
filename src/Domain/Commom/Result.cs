namespace Domain.Commom;

public class Result(bool isSuccess, string? error)
{
    private bool IsSuccess { get; } = isSuccess; //deu certo?
    protected bool IsFailure => !IsSuccess; //deu errado?
    protected string? Error { get; } = error; //qual foi o erro?

    public static Result Success() => new Result(true, null);

    public static Result Failure(string error) => new(false, error);

    // Recebe lambda que retorna Result simples (só valida)
    public Result Bind(Func<Result> func)
    {
        if (IsFailure)
            return this; // se falhou, para aqui
        return func(); // tudo ok, executa o próximo
    }

    // Recebe lambda que retorna Result<T> (valida e produz valor)
    public Result<T> Bind<T>(Func<Result<T>> func)
    {
        if (IsFailure)
            return Result<T>.Failure(Error!); // se falhou, para aqui e repassa o erro

        return func(); // tudo ok, produz o valor
    }

    // Constrói o objeto final quando não tem Value Objects
    public Result<T> Map<T>(Func<T> func)
    {
        if (IsFailure)
            return Result<T>.Failure(Error!);
        return Result<T>.Success(func()); // empacota o retorno da função dentro de um Result<T>
    }

    // Criado para validar Try/Catch dentro de Value Objects
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

    // Encadeia passando o valor adiante para outra operação
    public Result<K> Bind<K>(Func<T, Result<K>> func)
    {
        if (IsFailure)
            return Result<K>.Failure(Error!);

        return func(Value!); // passa o valor T para o próximo passo
    }

    // Transforma o valor T em K sem possibilidade de falha
    public Result<K> Map<K>(Func<T, K> func)    
    {
        if (IsFailure)
            return Result<K>.Failure(Error!);

        return Result<K>.Success(func(Value!)); // recebe T, devolve K empacotado
    }
}
