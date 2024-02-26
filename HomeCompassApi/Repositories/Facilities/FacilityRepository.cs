using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
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

        public IEnumerable<Facility> GetAll() => _context.Facilities.AsNoTracking().ToList();
        #region DTO
        /*
           var facilities = _context.Facilities.Select(f =>

           new FacilityDTO
           {
               Name = f.Name,
               Address = f.Location,
               PhoneNumber = f.ContactInformaton
           }

           );
           return facilities.Tolist(); */
        #endregion

        public Facility GetById(int id) => _context.Facilities.AsNoTracking().FirstOrDefault(f => f.Id == id);

        public bool IsExisted(Facility facility) => _context.Facilities.Contains(facility);


        public void Update(Facility entity)
        {
            _context.Facilities.Update(entity);
            _context.SaveChanges();
        }
    }
}
