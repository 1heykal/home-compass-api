using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.BLL.Cases
{
    public class MissingRepository : IRepository<Missing>
    {
        private readonly ApplicationDbContext _context;
        public MissingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Missing entity)
        {
            _context.Missings.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Missings.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Missing> GetAll() => _context.Missings.ToList();

        public Missing GetById(int id) => _context.Missings.FirstOrDefault(m => m.Id == id);

        public void Update(Missing entity)
        {
            _context.Missings.Update(entity);
            _context.SaveChanges();
        }
    }
}
