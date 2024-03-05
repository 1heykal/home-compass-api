using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Cases.Homeless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories.Cases
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
            return await _context.Homeless.Select(homeless => new HomelessDTO()
            {
                Id = homeless.Id,
                Name = homeless.FullName,
                Address = homeless.CurrentLocation,
                Description = homeless.AdditionalDetails,
                PhotoURL = homeless.PhotoUrl

            }).ToListAsync();
        }

        public async Task<List<HomelessDTO>> GetByPageAsync(PageDTO page) =>
            await _context.Homeless.Select(homeless => new HomelessDTO()
            {
                Id = homeless.Id,
                Name = homeless.FullName,
                Address = homeless.CurrentLocation,
                Description = homeless.AdditionalDetails,
                PhotoURL = homeless.PhotoUrl

            }).Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();

        public async Task<List<HomelessDTO>> GetByReporterId(string id)
        {
            return await _context.Homeless.Where(homeless => homeless.ReporterId == id).Select(homeless => new HomelessDTO()
            {
                Id = homeless.Id,
                Name = homeless.FullName,
                Address = homeless.CurrentLocation,
                Description = homeless.AdditionalDetails,
                PhotoURL = homeless.PhotoUrl

            }).ToListAsync();
        }

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
        public async Task<Homeless> GetById(int id) => await _context.Homeless.FindAsync(id);

        public async Task<bool> IsExisted(Homeless homeless) => await _context.Homeless.ContainsAsync(homeless);

        public async Task<bool> IsExisted(int id) => await _context.Homeless.AnyAsync(e => e.Id == id);

        public async Task Update(Homeless entity)
        {
            _context.Homeless.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
