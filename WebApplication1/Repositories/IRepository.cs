namespace WebApplication1
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}