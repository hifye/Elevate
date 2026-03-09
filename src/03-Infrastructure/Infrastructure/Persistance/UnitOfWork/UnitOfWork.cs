using System.Data;
using Application.Interfaces.UnitOfWork;

namespace Infrastructure.Persistance.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    public IDbTransaction Transaction { get; private set; }

    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
        Transaction = _connection.BeginTransaction();
    }

    /// <summary>
    /// Confirma a transação atual assincronamente, aplicando todas as alterações realizadas no banco de dados.
    /// Caso ocorra algum erro durante a confirmação, uma reversão será realizada automaticamente e a exceção será lançada.
    /// Após o commit, a transação atual será descartada e uma nova transação será iniciada.
    /// </summary>
    /// <returns>
    /// Uma tarefa representando a operação assíncrona de confirmação da transação.
    /// </returns>
    public Task CommitAsync()
    {
        try
        {
            Transaction.Commit();
            return Task.CompletedTask;
        }
        catch
        {
            Rollback();
            throw;
        }
        finally
        {
            Transaction.Dispose();
            Transaction = _connection.BeginTransaction();
        }
    }

    /// <summary>
    /// Reverte a transação atual, desfazendo todas as alterações realizadas no banco de dados
    /// desde o início da transação ou desde o último ponto de salvamento.
    /// Após a reversão, a transação atual será descartada e uma nova transação será iniciada.
    /// </summary>
    public void Rollback()
    {
        Transaction.Rollback();
        Transaction.Dispose();
        Transaction = _connection.BeginTransaction();
    }

    /// <summary>
    /// Libera os recursos associados à unidade de trabalho, incluindo a conexão com o banco de dados e a transação atual.
    /// Após a chamada deste método, a unidade de trabalho não poderá mais ser utilizada.
    /// Este método deve ser chamado ao final do ciclo de vida da unidade de trabalho para liberar recursos não gerenciados.
    /// </summary>
    public void Dispose()
    {
        Transaction?.Dispose();
        _connection?.Dispose();
    }
}