using DataAccessLayer.Context;
using DataAccessLayer.Entities;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IRepository<Message> Messages { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Messages = new Repository<Message>(_context);
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
