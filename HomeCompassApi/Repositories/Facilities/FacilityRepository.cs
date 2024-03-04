using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services.Cases.Homeless;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Facilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.BLL.Facilities
{
    public class FacilityRepository : IRepository<Facility>
    {
        private readonly ApplicationDbContext _context;
        public FacilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Facility entity)
        {
            await _context.Facilities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Facilities.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Facility>> GetAll() => await _context.Facilities.AsQueryable().AsNoTracking().ToListAsync();

        public async Task<List<Facility>> GetAllReduced()
        {
            return await _context.Facilities.AsQueryable().Select(f => FacilityToFacilityReduced(f)).ToListAsync();
        }

        private static Facility FacilityToFacilityReduced(Facility facility)
        {
            return new Facility()
            {
                Id = facility.Id,
                Name = facility.Name,
                ContactInformaton = facility.ContactInformaton,
                Description = facility.Description,
                Location = facility.Location,
                Resources = facility.Resources,
                Target = facility.Target
            };
        }

        public async Task<List<Facility>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Facilities.AsQueryable().Where(f => f.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Facility>> GetByPageAsync(PageDTO page) => await _context.Facilities.AsQueryable().Select(f => FacilityToFacilityReduced(f)).Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();

        public async Task<Facility> GetById(int id) => await _context.Facilities.AsQueryable().AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

        public async Task<bool> IsExisted(Facility facility) => await _context.Facilities.AsQueryable().ContainsAsync(facility);

        public async Task<bool> IsExisted(int id) => await _context.Facilities.AsQueryable().AnyAsync(e => e.Id == id);

        public async Task Update(Facility entity)
        {
            _context.Facilities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
