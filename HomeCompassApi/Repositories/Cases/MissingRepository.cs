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
        public void Add(Missing entity)
        {
            _context.Missings.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Missings.Remove(GetById(id));
            _context.SaveChanges();
        }

        public List<Missing> GetAll() => _context.Missings.AsNoTracking().ToList();

        public List<MissingDTO> GetAllReduced()
        {
            return _context.Missings.Select(m => new MissingDTO
            {
                Id = m.Id,
                Address = m.HomeAddress,
                Description = m.PhysicalDescription,
                MissingDate = m.DateOfDisappearance,
                Name = m.FullName,
                PhotoURL = m.PhotoUrl
            })
            .ToList();
        }

        public Missing GetById(int id) => _context.Missings.AsNoTracking().FirstOrDefault(m => m.Id == id);

        public bool IsExisted(Missing missing) => _context.Missings.Contains(missing);

        public void Update(Missing entity)
        {
            _context.Missings.Update(entity);
            _context.SaveChanges();
        }
    }
}
