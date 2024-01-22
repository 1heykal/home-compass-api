namespace HomeCompassApi.BLL
{
    public interface IRepository<T, T1>
    {
        void Create(T entity);
        IEnumerable<T> GetAll();
        T GetById(T1 id);
        void Update(T entity);
        void Delete(T1 id);
    }
}
