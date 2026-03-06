using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Infrastructure.Security;

/// <summary>
/// Classe responsável por realizar a criação e validação de hashes utilizando o algoritmo Argon2id.
/// </summary>
public class Argon2Hasher
{
    private const int SaltSize = 32;
    private const int HashSize = 32;
    private const int Iterations = 4;
    private const int Memory = 65536;
    private const int Parallelism = 8;

    /// <summary>
    /// Gera um hash utilizando o algoritmo Argon2id a partir de uma senha fornecida.
    /// </summary>
    /// <param name="password">
    /// Uma string representando a senha que será utilizada para gerar o hash.
    /// </param>
    /// <returns>
    /// Um valor de tupla contendo uma string que combina o salt e o hash gerados no formato Base64,
    /// e um array de bytes representando o salt utilizado na geração do hash.
    /// </returns>
    public (string Hash, byte[] Salt) Hash(string password)
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
        
        return (Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash), salt);
    }


    /// <summary>
    /// Verifica se a senha fornecida corresponde ao hash esperado utilizando o algoritmo Argon2id.
    /// </summary>
    /// <param name="password">
    /// Uma string representando a senha que será verificada.
    /// </param>
    /// <param name="salt">
    /// Um array de bytes representando o salt utilizado na criação do hash.
    /// </param>
    /// <param name="expectedHash">
    /// Um array de bytes contendo o hash esperado com o qual a senha será comparada.
    /// </param>
    /// <param name="iterations">
    /// O número de iterações utilizado no algoritmo Argon2id durante a criação do hash.
    /// </param>
    /// <param name="memory">
    /// A quantidade de memória (em kilobytes) utilizada pelo algoritmo Argon2id.
    /// </param>
    /// <param name="parallelism">
    /// O grau de paralelismo utilizado pelo algoritmo Argon2id.
    /// </param>
    /// <returns>
    /// Um booleano que indica se a senha fornecida corresponde ao hash esperado.
    /// </returns>
    public bool Verify(string password, byte[] salt, byte[] expectedHash, int iterations, int memory, int parallelism)
    {
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
    
    public static int DefaultIterations => Iterations;
    public static int DefaultMemory => Memory;
    public static int DefaultParallelism => Parallelism;
}