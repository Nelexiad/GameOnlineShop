namespace WebApplication1
{
    public interface IRepository<TDTO>
    {
        Task<TDTO> Get(int id);

        Task<IEnumerable<TDTO>> GetAll();

        Task<TDTO> Create(TDTO entity);
        Task<TDTO> Update(int id, TDTO entity);
        Task<bool> Delete(int id);
    }
}