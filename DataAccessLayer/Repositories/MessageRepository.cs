using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories;

public class MessageRepository : IMessageRepository
{
    private static List<Message> _messages = new();

    public Task AddAsync(Message message)
    {
        _messages.Add(message);
        return Task.CompletedTask;
    }

    public Task<List<Message>> GetAllAsync()
    {
        return Task.FromResult(_messages);
    }
}
