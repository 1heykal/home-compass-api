using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Services.Cases;
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
        public async Task Add(Missing entity)
        {
            await _context.Missings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Missings.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Missing>> GetAll() => await _context.Missings.AsNoTracking().ToListAsync();

        public async Task<List<MissingDTO>> GetAllReduced()
        {
            return await _context.Missings.Select(m => new MissingDTO
            {
                Id = m.Id,
                Address = m.HomeAddress,
                Description = m.PhysicalDescription,
                MissingDate = m.DateOfDisappearance,
                Name = m.FullName,
                PhotoURL = m.PhotoUrl
            })
            .ToListAsync();
        }

        public async Task<Missing> GetById(int id) => await _context.Missings.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> IsExisted(Missing missing) => await _context.Missings.ContainsAsync(missing);

        public async Task<bool> IsExisted(int id) => await _context.Missings.AnyAsync(e => e.Id == id);


        public async Task Update(Missing entity)
        {
            _context.Missings.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
