using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;

namespace HomeCompassApi.BLL.Facilities
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Categories.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll() => _context.Categories.ToList();

        public Category GetById(int id) => _context.Categories.FirstOrDefault(c => c.Id == id);


        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
        }
    }
}
