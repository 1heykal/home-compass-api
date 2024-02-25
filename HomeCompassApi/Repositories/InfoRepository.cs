using HomeCompassApi.BLL;
using HomeCompassApi.Models;

namespace HomeCompassApi.Repositories
{
    public class InfoRepository : IRepository<Info>
    {
        private readonly ApplicationDbContext _context;

        public InfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Info entity)
        {
            _context.Info.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Info.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Info> GetAll()
        {
            return _context.Info.ToList();
        }

        public Info GetById(int id)
        {
            return _context.Info.FirstOrDefault(i => i.Id == id);
        }

        public void Update(Info entity)
        {
            _context.Info.Update(entity);
            _context.SaveChanges();
        }
    }
}
