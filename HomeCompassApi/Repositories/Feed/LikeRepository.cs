using HomeCompassApi.DbContexts;
using HomeCompassApi.Entities;
using HomeCompassApi.Entities.Feed;
using HomeCompassApi.Models;
using HomeCompassApi.Repositories;
using HomeCompassApi.Services;
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

        public async Task<List<Like>> GetByPostId(int id) => await _context.Likes.AsNoTracking().Where(l => l.PostId == id).ToListAsync();

        public async Task<bool> IsExisted(Like like) => await _context.Likes.ContainsAsync(like);

        public async Task<List<Like>> GetByPageAsync(int postId, PageDTO page)
        {
            return await _context.Likes.Where(l => l.PostId == postId).Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }

        public async Task Delete(Like like)
        {
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }


    }
}
