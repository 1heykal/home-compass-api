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

        public void Add(Like entity)
        {
            _context.Likes.Add(entity);
            _context.SaveChanges();
        }

        public List<Like> GetByPostId(int id) => _context.Likes.Where(l => l.PostId == id).AsNoTracking().ToList();

        public bool IsExisted(Like like) => _context.Likes.Contains(like);

        public void Delete(Like like)
        {
            _context.Likes.Remove(like);
            _context.SaveChanges();
        }


    }
}
