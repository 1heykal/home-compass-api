using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories.Feed
{
    public class LikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Like entity)
        {
            await _context.Likes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Like>> GetByPostId(int id) => await _context.Likes.Where(l => l.PostId == id).AsNoTracking().ToListAsync();

        public async Task<bool> IsExisted(Like like) => await _context.Likes.ContainsAsync(like);

        public async Task Delete(Like like)
        {
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }


    }
}
