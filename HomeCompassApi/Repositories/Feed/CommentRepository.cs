using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services.Feed;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.BLL
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAll() => await _context.Comments.AsNoTracking().ToListAsync();

        public async Task<List<CommentDTO>> GetAllReduced()
        {
            return await _context.Comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Content = c.Content,
                AuthorName = $"{c.User.FirstName} {c.User.LastName}",
                AuthorPhotoURL = c.User.PhotoUrl
            }
            ).ToListAsync();
        }

        public async Task<Comment> GetById(int id) => await _context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        public async Task<bool> IsExisted(Comment comment) => await _context.Comments.ContainsAsync(comment);

        public async Task<bool> IsExisted(int id) => await _context.Comments.AnyAsync(e => e.Id == id);


        public async Task Update(Comment entity)
        {
            _context.Comments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Comments.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }


    }
}
