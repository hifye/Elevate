using System.Data;

namespace Application.Contracts.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IDbTransaction Transaction { get; }
    Task CommitAsync();
    void Rollback();
}