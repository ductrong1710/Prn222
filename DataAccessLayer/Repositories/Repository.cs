using DataAccessLayer.Context;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }
}
