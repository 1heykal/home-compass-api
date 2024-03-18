using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Facilities;
using Microsoft.EntityFrameworkCore;


namespace HomeCompassApi.Repositories.Facilities
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
            var facility = await _context.Facilities
                .Include(f => f.Jobs)
                .Include(f => f.Resources)
                .FirstOrDefaultAsync(f => f.Id == id);

            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Facility>> GetAll() => await _context.Facilities.AsNoTracking().ToListAsync();

        public async Task<List<ReadFacilitiesDTO>> GetAllReduced()
        {
            return await _context.Facilities.Select(facility => new ReadFacilitiesDTO()
            {
                Id = facility.Id,
                Name = facility.Name,
                ContactInformaton = facility.ContactInformaton,
                Description = facility.Description,
                Location = facility.Location,
                Resources = facility.Resources,
                Target = facility.Target
            }).ToListAsync();
        }

        private static ReadFacilitiesDTO FacilityToReadFacilitiesDTO(Facility facility)
        {
            return new ReadFacilitiesDTO()
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
            return await _context.Facilities.Where(f => f.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Facility>> GetByContributorIdAsync(string id)
        {
            return await _context.Facilities.Where(f => f.ContributorId == id).ToListAsync();
        }

        public async Task<List<ReadFacilitiesDTO>> GetByPageAsync(PageDTO page) => await _context.Facilities.Select(facility => new ReadFacilitiesDTO()
        {
            Id = facility.Id,
            Name = facility.Name,
            ContactInformaton = facility.ContactInformaton,
            Description = facility.Description,
            Location = facility.Location,
            Resources = facility.Resources,
            Target = facility.Target

        }).Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();



        public async Task<Facility> GetById(int id) => await _context.Facilities.Include(f => f.Resources).FirstOrDefaultAsync(f => f.Id == id);

        public async Task<bool> IsExisted(Facility facility) => await _context.Facilities.ContainsAsync(facility);

        public async Task<bool> IsExisted(int id) => await _context.Facilities.AnyAsync(e => e.Id == id);

        public async Task Update(Facility entity)
        {
            _context.Facilities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
