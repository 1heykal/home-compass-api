using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services.Facilities;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories.Facilities
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
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if(category is not null)
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAll() => await _context.Categories.AsNoTracking().ToListAsync();

        public async Task<List<CategoryDTO>> GetAllReduced() => await _context.Categories.AsNoTracking().Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name

        }).ToListAsync();

        public async Task<Category> GetById(int id) => await _context.Categories.FindAsync(id);

        public async Task<bool> IsExisted(Category category) => await _context.Categories.ContainsAsync(category);

        public async Task<bool> IsExisted(int id)   => await _context.Categories.AnyAsync(e => e.Id == id);

        public async Task<bool> NameExists(int id, string name) => await _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.Id != id);
        public async Task<bool> NameExists(string name) => await _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task Update(Category entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
