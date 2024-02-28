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
        public void Add(Facility entity)
        {
            _context.Facilities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Facilities.Remove(GetById(id));
            _context.SaveChanges();
        }

        public List<Facility> GetAll() => _context.Facilities.AsNoTracking().ToList();

        public List<FacilityDTO> GetAllReduced()
        {
            return _context.Facilities.Select(f => new FacilityDTO
            {
                Id = f.Id,
                Name = f.Name,
                ContactInformation = f.ContactInformaton,
                Description = f.Description,
                Location = f.Location,
                Resources = f.Resources,
                Target = f.Target

            }).ToList();
        }

        public Facility GetById(int id) => _context.Facilities.AsNoTracking().FirstOrDefault(f => f.Id == id);

        public bool IsExisted(Facility facility) => _context.Facilities.Contains(facility);


        public void Update(Facility entity)
        {
            _context.Facilities.Update(entity);
            _context.SaveChanges();
        }
    }
}
