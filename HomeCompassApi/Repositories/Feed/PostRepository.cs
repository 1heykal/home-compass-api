using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services;
using HomeCompassApi.Services.CRUD;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace HomeCompassApi.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PostRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(Post entity)
        {
            await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAll() => await _context.Posts.AsNoTracking().ToListAsync();

        public async Task<List<ReadAllPostsDTO>> GetAllReduced()
        {
            return await _context.Posts.ProjectTo<ReadAllPostsDTO>(_mapper.ConfigurationProvider).
                OrderByDescending(p => p.PublisedOn).
                ToListAsync();
        }
        

        public async Task<List<ReadAllPostsDTO>> GetByUserIdAsync(string id)
        {
            return await _context.Posts.Where(p => p.UserId == id)
                .ProjectTo<ReadAllPostsDTO>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.PublisedOn).
                ToListAsync();
        }
        
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        
        public async Task<Post> GetById(int id) => await _context.Posts.FindAsync(id);

        public async Task<List<ReadPostDTO>> GetByIdDTO(int id)
        {
            return await _context.Posts.Where(p => p.Id == id).ProjectTo<ReadPostDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
        
        public async Task<List<ReadAllPostsDTO>> GetByPageAsync(PageDTO page)
        {
            return await _context.Posts.ProjectTo<ReadAllPostsDTO>(_mapper.ConfigurationProvider)
            .OrderByDescending(p => p.PublisedOn).Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }

        public async Task Update(Post entity)
        {
            _context.Posts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == id);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(Post post) => await _context.Posts.ContainsAsync(post);
        public async Task<bool> IsExisted(int id) => await _context.Posts.AnyAsync(p => p.Id == id);


    }
}
