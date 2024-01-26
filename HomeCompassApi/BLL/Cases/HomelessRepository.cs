using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;

namespace HomeCompassApi.BLL.Cases
{
    public class HomelessRepository : IRepository<Homeless>
    {
        private readonly ApplicationDbContext _context;
        public HomelessRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Homeless entity)
        {
            _context.Homeless.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Homeless.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Homeless> GetAll() => _context.Homeless.ToList();

        public Homeless GetById(int id) => _context.Homeless.FirstOrDefault(h => h.Id == id);


        public void Update(Homeless entity)
        {
            _context.Homeless.Update(entity);
            _context.SaveChanges();

        }
    }
}
