namespace HomeCompassApi.BLL
{
    public interface IRepository<T>
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Delete(int id);
    }
}
