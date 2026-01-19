using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
    }

    public async Task<List<Message>> GetAllAsync()
    {
        return await _context.Messages.OrderByDescending(m => m.CreatedAt).ToListAsync();
    }

    public async Task<Message?> GetByIdAsync(int id)
    {
        return await _context.Messages.FindAsync(id);
    }
}
