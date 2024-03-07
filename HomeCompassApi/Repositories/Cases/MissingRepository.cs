using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Cases;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories.Cases
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
            return await _context.Missings.Select(missing => new MissingDTO()
            {
                Id = missing.Id,
                Address = missing.HomeAddress,
                Description = missing.PhysicalDescription,
                MissingDate = missing.DateOfDisappearance,
                Name = missing.FullName,
                PhotoURL = missing.PhotoUrl

            }).ToListAsync();
        }

        private static MissingDTO MissingToMissingDTO(Missing missing)
        {
            return new MissingDTO()
            {
                Id = missing.Id,
                Address = missing.HomeAddress,
                Description = missing.PhysicalDescription,
                MissingDate = missing.DateOfDisappearance,
                Name = missing.FullName,
                PhotoURL = missing.PhotoUrl
            };
        }

        public async Task<List<MissingDTO>> GetByReporterId(string id)
        {
            return await _context.Missings.Where(missing => missing.ReporterId == id).Select(missing => new MissingDTO()
            {
                Id = missing.Id,
                Address = missing.HomeAddress,
                Description = missing.PhysicalDescription,
                MissingDate = missing.DateOfDisappearance,
                Name = missing.FullName,
                PhotoURL = missing.PhotoUrl

            }).ToListAsync();
        }

        public async Task<List<Missing>> GetByPageAsync(PageDTO page)
        {
            return await _context.Missings.Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }

        public async Task<Missing> GetById(int id) => await _context.Missings.FindAsync(id);

        public async Task<bool> IsExisted(Missing missing) => await _context.Missings.ContainsAsync(missing);

        public async Task<bool> IsExisted(int id) => await _context.Missings.AnyAsync(e => e.Id == id);

        public async Task<MatchDTO> GetMatchDetails(int id)
        {
            return await _context.Homeless.Select(m => new MatchDTO
            {
                Id = m.Id,
                Name = m.FullName,
                Address = m.CurrentLocation,
                Phone = m.PhoneNumber

            }).FirstOrDefaultAsync(m => m.Id == id);
        }


        public async Task Update(Missing entity)
        {
            _context.Missings.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
