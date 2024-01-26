using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;

namespace HomeCompassApi.BLL.Facilities
{
    public class WorkRepository : IRepository<Work>
    {
        private readonly ApplicationDbContext _context;
        public WorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Work entity)
        {
            _context.Facilities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Work> GetAll()
        {
            throw new NotImplementedException();
        }

        public Work GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Work entity)
        {
            throw new NotImplementedException();
        }
    }
}
