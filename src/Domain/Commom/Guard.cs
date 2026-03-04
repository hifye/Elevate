namespace Domain.Commom;

public static class Guard
{
    // Se Value for vazio ou nulo, falha com a mensagem fornecida.
    public static Result AgainstNullOrWhiteSpace(string value, string message)
    {
        if (String.IsNullOrWhiteSpace(value))
            return Result.Failure(message);

        return Result.Success();
    }

    // Se condition for verdadeira, falha com a mensagem fornecida.
    public static Result AgainstOutOfRange(bool condition, string message)
    {
        if (condition)
            return Result.Failure(message);

        return Result.Success();
    }
}
