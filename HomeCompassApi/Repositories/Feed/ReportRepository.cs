using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories.Feed
{
    public class ReportRepository : IRepository<Report>
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Report entity)
        {
            await _context.Reports.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Report>> GetAll()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> GetById(int id)
        {
            return await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Update(Report entity)
        {
            _context.Reports.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var report = await GetById(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(Report entity)
        {
            return await _context.Reports.ContainsAsync(entity);
        }
    }
}
