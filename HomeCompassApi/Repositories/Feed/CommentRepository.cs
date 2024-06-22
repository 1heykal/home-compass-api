using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Feed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CommentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAll() => await _context.Comments.AsNoTracking().ToListAsync();

        public async Task<List<CommentDTO>> GetAllReduced()
        {
            return await _context.Comments.ProjectTo<CommentDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }


        public async Task<List<CommentDTO>> GetByPageAsync(int postId, PageDTO page)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }

        public async Task<List<CommentDTO>> GetByPostId(int id)
        {
            return await _context.Comments
                .Where(c => c.PostId == id)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        
        public async Task<Comment> GetById(int id) => await _context.Comments.FindAsync(id);
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
