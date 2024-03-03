using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories
{
    public class InfoRepository : IRepository<Info>
    {
        private readonly ApplicationDbContext _context;

        public InfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Info entity)
        {
            await _context.Info.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Info.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Info>> GetAll()
        {
            return await _context.Info.ToListAsync();
        }

        public async Task<Info> GetById(int id)
        {
            return await _context.Info.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> IsExisted(Info entity)
        {
            return await _context.Info.ContainsAsync(entity);
        }

        public async Task<bool> IsExisted(int id) => await _context.Info.FindAsync(id) is not null;


        public async Task Update(Info entity)
        {
            _context.Info.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
