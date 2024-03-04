using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Feed;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<Comment>> GetAll() => await _context.Comments.AsQueryable().AsNoTracking().ToListAsync();

        public async Task<List<CommentDTO>> GetAllReduced()
        {
            return await _context.Comments.AsQueryable().Include(c => c.User).Select(c => CommentToCommentDTO(c)).ToListAsync();
        }


        public async Task<List<CommentDTO>> GetByPageAsync(int postId, PageDTO page)
        {
            return await _context.Comments.AsQueryable().Include(c => c.User).
                Where(c => c.PostId == postId).
                Select(c => CommentToCommentDTO(c)).
                Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }

        private static CommentDTO CommentToCommentDTO(Comment comment)
        {
            return new CommentDTO()
            {
                Id = comment.Id,
                Content = comment.Content,
                AuthorName = $"{comment.User.FirstName} {comment.User.LastName}",
                AuthorPhotoURL = comment.User.PhotoUrl
            };
        }

        public async Task<Comment> GetById(int id) => await _context.Comments.AsQueryable().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        public async Task<bool> IsExisted(Comment comment) => await _context.Comments.AsQueryable().ContainsAsync(comment);

        public async Task<bool> IsExisted(int id) => await _context.Comments.AsQueryable().AnyAsync(e => e.Id == id);

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
