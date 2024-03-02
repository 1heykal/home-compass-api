using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services.Facilities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Facility>> GetAll() => await _context.Facilities.AsNoTracking().ToListAsync();

        public async Task<List<FacilityDTO>> GetAllReduced()
        {
            return await _context.Facilities.Select(f => new FacilityDTO
            {
                Id = f.Id,
                Name = f.Name,
                ContactInformation = f.ContactInformaton,
                Description = f.Description,
                Location = f.Location,
                Resources = f.Resources,
                Target = f.Target

            }).ToListAsync();
        }

        public async Task<Facility> GetById(int id) => await _context.Facilities.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

        public async Task<bool> IsExisted(Facility facility) => await _context.Facilities.ContainsAsync(facility);


        public async Task Update(Facility entity)
        {
            _context.Facilities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
