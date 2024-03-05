using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.CRUD;
using HomeCompassApi.Services.Feed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace HomeCompassApi.Repositories
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

            }).ToListAsync();
        }

        private static ReadAllPostsDTO PostToReadAllPostsDTO(Post p)
        {
            return new ReadAllPostsDTO
            {
                Id = p.Id,
                AuthorName = $"{p.User.FirstName} {p.User.LastName}",
                Content = p.Content,
                Title = p.Title,
                LikesCount = p.Likes.Count,
                AuthorPhotoUrl = p.User.PhotoUrl,
                CommentsCount = p.Comments.Count
            };
        }

        public async Task<List<ReadAllPostsDTO>> GetByUserIdAsync(string id)
        {
            return await _context.Posts.Where(p => p.UserId == id).Select(p => new ReadAllPostsDTO
            {
                Id = p.Id,
                AuthorName = $"{p.User.FirstName} {p.User.LastName}",
                Content = p.Content,
                Title = p.Title,
                LikesCount = p.Likes.Count,
                AuthorPhotoUrl = p.User.PhotoUrl,
                CommentsCount = p.Comments.Count

            }).ToListAsync();
        }



        public async Task<Post> GetById(int id) => await _context.Posts.FindAsync(id);

        public async Task<List<ReadPostDTO>> GetByIdDTO(int id)
        {
            return await _context.Posts.Where(p => p.Id == id).Select(post => new ReadPostDTO
            {
                Id = post.Id,
                Content = post.Content,
                Title = post.Title,
                LikesCount = post.Likes.Count,
                CommentsCount = post.Comments.Count,
                Archived = post.Archived,
                PublisedOn = post.PublisedOn,
                UserId = post.UserId

            }).ToListAsync();
        }


        private static ReadPostDTO PostToReadPostDTO(Post post)
        {
            return new ReadPostDTO
            {
                Id = post.Id,
                Content = post.Content,
                Title = post.Title,
                LikesCount = post.Likes.Count,
                CommentsCount = post.Comments.Count,
                Archived = post.Archived,
                PublisedOn = post.PublisedOn,
                UserId = post.UserId
            };
        }




        public async Task<List<ReadAllPostsDTO>> GetByPageAsync(PageDTO page)
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

            }).Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }

        public async Task Update(Post entity)
        {
            _context.Posts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts.AsQueryable()
                .Include(p => p.Comments)
                .Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == id);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(Post post) => await _context.Posts.ContainsAsync(post);

        public async Task<bool> IsExisted(int id) => await _context.Posts.AnyAsync(p => p.Id == id);


    }
}
