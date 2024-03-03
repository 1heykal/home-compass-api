using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services.CRUD;
using HomeCompassApi.Services.Feed;
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

        public async Task Add(Post entity)
        {
            await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAll() => await _context.Posts.AsNoTracking().ToListAsync();

        public async Task<List<ReadAllPostsDTO>> GetAllReduced()
        {
            return await _context.Posts.Select(p => new ReadAllPostsDTO
            {
                Id = p.Id,
                AuthorName = $"{p.User.FirstName} {p.User.LastName}",
                Content = p.Content,
                Title = p.Title,
                LikesCount = p.Likes.Count,
                AuthorPhotoUrl = p.User.PhotoUrl,
                CommentsCount = p.Comments.Count
            }
            ).ToListAsync();
        }

        public async Task<Post> GetById(int id) => await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task Update(Post entity)
        {
            _context.Posts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
                    if (post != null)
                    {
                        _context.Comments.RemoveRange(_context.Comments.Where(c => c.PostId == post.Id));
                        _context.Likes.RemoveRange(_context.Likes.Where(l => l.PostId == post.Id));
                        _context.Posts.Remove(post);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task<bool> IsExisted(Post post) => await _context.Posts.ContainsAsync(post);

        public async Task<bool> IsExisted(int id) => await _context.Posts.AnyAsync(p => p.Id == id);


    }
}
