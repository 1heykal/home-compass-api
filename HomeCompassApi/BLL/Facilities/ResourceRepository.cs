using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;

namespace HomeCompassApi.BLL.Facilities
{
    public class ResourceRepository : IRepository<Resource>
    {
        private readonly ApplicationDbContext _context;
        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Resource entity)
        {
            _context.Resources.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Resources.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Resource> GetAll() => _context.Resources.ToList();

        public Resource GetById(int id) => _context.Resources.FirstOrDefault(r => r.Id == id);

        public void Update(Resource entity)
        {
            _context.Resources.Update(entity);
            _context.SaveChanges();
        }
    }
}
