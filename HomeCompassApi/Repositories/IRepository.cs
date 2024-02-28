namespace HomeCompassApi.BLL
{
    public interface IRepository<T>
    {
        void Add(T entity);
        List<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Delete(int id);
        bool IsExisted(T entity);
    }
}
