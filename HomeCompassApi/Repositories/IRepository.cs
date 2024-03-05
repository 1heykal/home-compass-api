namespace HomeCompassApi.Repositories
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task Update(T entity);
        Task Delete(int id);
        Task<bool> IsExisted(T entity);
    }
}
