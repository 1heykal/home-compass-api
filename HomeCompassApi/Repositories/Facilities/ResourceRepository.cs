using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services.Facilities;
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

        public async Task<List<ResourceDTO>> GetAllReduced() => await _context.Resources.AsNoTracking().Select(r => new ResourceDTO
        {
            Id = r.Id,
            Name = r.Name,
            IsAvailable = r.IsAvailable
        }).ToListAsync();


        public async Task<Resource> GetById(int id) => await _context.Resources.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

        public async Task<bool> IsExisted(Resource resource) => await _context.Resources.ContainsAsync(resource);

        public async Task<bool> IsExisted(int id) => await _context.Resources.AnyAsync(e => e.Id == id);


        public async Task<bool> NameExists(int id, string name) => await _context.Resources.AnyAsync(r => r.Name.ToLower() == name.ToLower() && r.Id != id);

        public async Task<bool> NameExists(string name) => await _context.Resources.AnyAsync(r => r.Name.ToLower() == name.ToLower());


        public async Task Update(Resource entity)
        {
            _context.Resources.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
