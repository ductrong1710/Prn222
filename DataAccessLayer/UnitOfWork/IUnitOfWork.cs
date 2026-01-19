using DataAccessLayer.Repositories;

namespace DataAccessLayer.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IMessageRepository Messages { get; }
    Task<int> SaveChangesAsync();
}
