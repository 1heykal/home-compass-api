using HomeCompassApi.BLL;
using HomeCompassApi.Models;

namespace HomeCompassApi.Repositories.Feed
{
    public class ReportRepository : IRepository<Report>
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Report entity)
        {
            _context.Reports.Add(entity);
            _context.SaveChanges();
        }

        public List<Report> GetAll()
        {
            return _context.Reports.ToList();
        }

        public Report GetById(int id)
        {
            return _context.Reports.FirstOrDefault(r => r.Id == id);
        }

        public void Update(Report entity)
        {
            _context.Reports.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var report = GetById(id);
            _context.Reports.Remove(report);
            _context.SaveChanges();
        }

        public bool IsExisted(Report entity)
        {
            return _context.Reports.Contains(entity);
        }
    }
}
