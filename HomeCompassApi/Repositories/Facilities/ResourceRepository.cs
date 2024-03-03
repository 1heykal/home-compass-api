using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.BLL.Facilities
{
    public class ResourceRepository : IRepository<Resource>
    {
        private readonly ApplicationDbContext _context;
        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Resource entity)
        {
            await _context.Resources.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Resources.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Resource>> GetAll() => await _context.Resources.AsNoTracking().ToListAsync();

        public async Task<Resource> GetById(int id) => await _context.Resources.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

        public async Task<bool> IsExisted(Resource resource) => await _context.Resources.ContainsAsync(resource);

        public async Task<bool> IsExisted(int id) => await _context.Resources.AnyAsync(e => e.Id == id);


        public async Task<bool> NameExists(string name) => await _context.Resources.FirstOrDefaultAsync(r => r.Name.Equals(name)) is not null;

        public async Task Update(Resource entity)
        {
            _context.Resources.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
