using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Services.Cases.Homeless;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.BLL.Cases
{
    public class HomelessRepository : IRepository<Homeless>
    {
        private readonly ApplicationDbContext _context;
        public HomelessRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Homeless entity)
        {
            await _context.Homeless.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Homeless.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Homeless>> GetAll() => await _context.Homeless.AsNoTracking().ToListAsync();

        public async Task<List<HomelessDTO>> GetAllReduced()
        {
            return await _context.Homeless.Select(h => new HomelessDTO
            {
                Id = h.Id,
                Name = h.FullName,
                Address = h.CurrentLocation,
                Description = h.AdditionalDetails,
                PhotoURL = h.PhotoUrl
            }
            ).ToListAsync();
        }
        public async Task<Homeless> GetById(int id) => await _context.Homeless.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

        public async Task<bool> IsExisted(Homeless homeless) => await _context.Homeless.ContainsAsync(homeless);

        public async Task<bool> IsExisted(int id) => await _context.Homeless.FindAsync(id) is not null;

        public async Task Update(Homeless entity)
        {
            _context.Homeless.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
