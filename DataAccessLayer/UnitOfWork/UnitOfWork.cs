using DataAccessLayer.Repositories;

namespace DataAccessLayer.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IMessageRepository Messages { get; }

    public UnitOfWork(IMessageRepository messages)
    {
        Messages = messages;
    }
}
