using System.Security.Cryptography;
using System.Text;
using Application.Interfaces.Services;
using Konscious.Security.Cryptography;

namespace Infrastructure.Security;

/// <summary>
/// Classe responsável por realizar o hash de senhas e verificar se uma senha corresponde ao hash armazenado,
/// utilizando o algoritmo Argon2id.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 32;
    private const int HashSize = 32;
    private const int Iterations = 4;
    private const int Memory = 65536;
    private const int Parallelism = 8;

    /// <summary>
    /// Gera um hash seguro para a senha fornecida utilizando o algoritmo Argon2id.
    /// </summary>
    /// <param name="password">
    /// A senha em texto puro que será utilizada para gerar o hash.
    /// </param>
    /// <returns>
    /// Retorna uma string representando o hash da senha, no formato
    /// "argon2id$iterações$memória$paralelismo$salt$hash".
    /// </returns>
    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            Iterations = Iterations,
            MemorySize = Memory,
            DegreeOfParallelism = Parallelism
        };

        var hash = argon2.GetBytes(HashSize);

        return string.Join("$", "argon2id",
            Iterations,
            Memory,
            Parallelism,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash)
        );
    }

    /// <summary>
    /// Verifica se a senha fornecida corresponde ao hash armazenado utilizando o algoritmo Argon2id.
    /// </summary>
    /// <param name="password">
    /// A senha em texto puro que será verificada.
    /// </param>
    /// <param name="hashedPassword">
    /// O hash previamente armazenado no formato "argon2id$iterações$memória$paralelismo$salt$hash".
    /// </param>
    /// <returns>
    /// Retorna verdadeiro se a senha fornecida corresponde ao hash armazenado.
    /// Caso contrário, retorna falso.
    /// </returns>
    public bool VerifyPassword(string password, string hashedPassword)
    {
        var parts = hashedPassword.Split("$");
        if (parts.Length != 6)
            return false;
        
        if (parts[0] != "argon2id")
            return false;

        try
        {
            var iterations = int.Parse(parts[1]);
            var memory = int.Parse(parts[2]);
            var parallelism = int.Parse(parts[3]);
            var salt = Convert.FromBase64String(parts[4]);
            var expectedHash = Convert.FromBase64String(parts[5]);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                Iterations = iterations,
                MemorySize = memory,
                DegreeOfParallelism = parallelism
            };
            
            var computedHash = argon2.GetBytes(expectedHash.Length);
            
            return CryptographicOperations.FixedTimeEquals(expectedHash, computedHash);
        }
        catch
        {
            return false;
        }
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
        
        if (parts[0] != "argon2id")
            return false;

        var iterations = int.Parse(parts[1]);
        var memory = int.Parse(parts[2]);
        var parallelism = int.Parse(parts[3]);

        return iterations < Iterations || iterations > Iterations
               || memory < Memory || memory > Memory
               || parallelism < Parallelism || parallelism > Parallelism;
    }
}