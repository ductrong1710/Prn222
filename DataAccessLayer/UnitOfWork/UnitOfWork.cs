using DataAccessLayer.Repositories;

namespace DataAccessLayer.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IMessageRepository? _messages;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IMessageRepository Messages => _messages ??= new MessageRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
