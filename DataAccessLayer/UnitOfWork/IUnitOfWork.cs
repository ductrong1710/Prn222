using DataAccessLayer.Entities;

public interface IUnitOfWork : IDisposable
{
    IRepository<Message> Messages { get; }
    int Save();
}
