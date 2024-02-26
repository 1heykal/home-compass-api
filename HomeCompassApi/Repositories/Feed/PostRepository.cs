using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.BLL
{
    public class PostRepository : IRepository<Post>
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Post entity)
        {
            _context.Posts.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAll() => _context.Posts.AsNoTracking().ToList();

        public Post GetById(int id) => _context.Posts.AsNoTracking().FirstOrDefault(p => p.Id == id);

        public void Update(Post entity)
        {
            _context.Posts.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var post = _context.Posts.FirstOrDefault(p => p.Id == id);
                    if (post != null)
                    {
                        _context.Comments.RemoveRange(_context.Comments.Where(c => c.PostId == post.Id));
                        _context.Likes.RemoveRange(_context.Likes.Where(l => l.PostId == post.Id));
                        _context.Posts.Remove(post);
                        _context.SaveChanges();
                        transaction.Commit();
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public bool IsExisted(Post post) => _context.Posts.Contains(post);

    }
}
