using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    Task<List<Message>> GetAllAsync();
}
