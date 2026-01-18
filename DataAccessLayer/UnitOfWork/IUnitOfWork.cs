using DataAccessLayer.Repositories;

namespace DataAccessLayer.UnitOfWork;

public interface IUnitOfWork
{
    IMessageRepository Messages { get; }
}
