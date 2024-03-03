using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.BLL.Facilities
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Categories.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAll() => await _context.Categories.AsNoTracking().ToListAsync();

        public async Task<Category> GetById(int id) => await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        public async Task<bool> IsExisted(Category category) => await _context.Categories.ContainsAsync(category);

        public async Task<bool> IsExisted(int id) => await _context.Categories.FindAsync(id) is not null;


        public async Task<bool> NameExists(string name) => await _context.Categories.FirstOrDefaultAsync(r => r.Name.Equals(name)) is not null;

        public async Task Update(Category entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
