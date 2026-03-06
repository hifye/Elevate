using Application.Interfaces.Services;

namespace Infrastructure.Security;

/// <summary>
/// Classe responsável por realizar operações de hash e validação de senhas
/// utilizando o algoritmo Argon2id com parâmetros configuráveis.
/// </summary>
public class PasswordHasher(Argon2Hasher argon2Hasher) : IPasswordHasher
{
    private readonly Argon2Hasher _argon2 = argon2Hasher;

    /// <summary>
    /// Gera um hash de uma senha utilizando o algoritmo Argon2id com parâmetros padrão.
    /// </summary>
    /// <param name="password">
    /// Uma string representando a senha que será utilizada para gerar o hash.
    /// </param>
    /// <returns>
    /// Uma string contendo o hash gerado no formato "argon2id$iterações$memória$paralelismo$salt$hash",
    /// onde cada valor é separado pelo caractere "$".
    /// </returns>
    public string HashPassword(string password)
    {
        var result = _argon2.Hash(password);

        return string.Join("$", "argon2id",
            Argon2Hasher.DefaultIterations,
            Argon2Hasher.DefaultMemory,
            Argon2Hasher.DefaultParallelism,
            Convert.ToBase64String(result.Salt), result.Hash);
    }

    /// <summary>
    /// Verifica se uma senha fornecida corresponde ao hash armazenado utilizando o algoritmo Argon2id.
    /// </summary>
    /// <param name="password">
    /// Uma string representando a senha que será verificada.
    /// </param>
    /// <param name="hashedPassword">
    /// Uma string contendo o hash da senha armazenado no formato "argon2id$iterações$memória$paralelismo$salt$hash".
    /// </param>
    /// <returns>
    /// Um valor booleano indicando se a senha fornecida corresponde ao hash armazenado.
    /// Retorna true se as credenciais coincidem; caso contrário, retorna false.
    /// </returns>
    public bool VerifyPassword(string password, string hashedPassword)
    {
        var parts = hashedPassword.Split("$");
        if (parts.Length != 6)
            return false;

        var iterations = int.Parse(parts[1]);
        var memory = int.Parse(parts[2]);
        var parallelism = int.Parse(parts[3]);
        var salt = Convert.FromBase64String(parts[4]);
        var hash = Convert.FromBase64String(parts[5]);

        return _argon2.Verify(password, salt, hash, iterations, memory, parallelism);
    }

    /// <summary>
    /// Verifica se o hash fornecido requer um novo hash com base em parâmetros padrão do algoritmo Argon2id.
    /// </summary>
    /// <param name="hashedPassword">
    /// Uma string contendo o hash previamente gerado no formato "argon2id$iterações$memória$paralelismo$salt$hash".
    /// </param>
    /// <returns>
    /// Retorna verdadeiro se o hash precisar ser atualizado para atender aos parâmetros padrão atuais.
    /// Caso contrário, retorna falso.
    /// </returns>
    public bool NeedsRehash(string hashedPassword)
    {
        var parts = hashedPassword.Split("$");
        if (parts.Length != 6)
            return false;

        var iterations = int.Parse(parts[1]);
        var memory = int.Parse(parts[2]);
        var parallelism = int.Parse(parts[3]);

        return iterations < Argon2Hasher.DefaultIterations 
               || memory < Argon2Hasher.DefaultMemory 
               || parallelism < Argon2Hasher.DefaultParallelism;
    }
}