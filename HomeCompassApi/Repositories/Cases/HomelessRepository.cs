using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Services;
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

        public async Task<List<Homeless>> GetAll() => await _context.Homeless.AsQueryable().AsNoTracking().ToListAsync();

        public async Task<List<HomelessDTO>> GetAllReduced()
        {
            return await _context.Homeless.AsQueryable().Select(h => HomelessToHomelessDTO(h)).ToListAsync();
        }

        public async Task<List<HomelessDTO>> GetByPageAsync(PageDTO page) =>
            await _context.Homeless.AsQueryable().Select(h => HomelessToHomelessDTO(h)).Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();


        private static HomelessDTO HomelessToHomelessDTO(Homeless homeless)
        {
            return new HomelessDTO()
            {
                Id = homeless.Id,
                Name = homeless.FullName,
                Address = homeless.CurrentLocation,
                Description = homeless.AdditionalDetails,
                PhotoURL = homeless.PhotoUrl
            };
        }
        public async Task<Homeless> GetById(int id) => await _context.Homeless.AsQueryable().AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

        public async Task<bool> IsExisted(Homeless homeless) => await _context.Homeless.AsQueryable().ContainsAsync(homeless);

        public async Task<bool> IsExisted(int id) => await _context.Homeless.AsQueryable().AnyAsync(e => e.Id == id);

        public async Task Update(Homeless entity)
        {
            _context.Homeless.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
