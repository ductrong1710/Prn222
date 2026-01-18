public interface IRepository<T> where T : class
{
    void Add(T entity);
    IEnumerable<T> GetAll();
}
