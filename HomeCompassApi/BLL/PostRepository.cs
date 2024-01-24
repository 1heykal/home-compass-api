﻿using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.BLL
{
    public class PostRepository : IRepository<Post, Guid>
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Post entity)
        {
            _context.Posts.Add(entity);
        }

        public IEnumerable<Post> GetAll() => _context.Posts.ToList();

        public Post GetById(Guid id) => _context.Posts.FirstOrDefault(p => p.Id == id);

        public void Update(Post entity)
        {
            _context.Posts.Update(entity);
        }

        public void Delete(Guid id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var post = _context.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
                    if (post != null)
                    {
                        _context.Comments.RemoveRange(post.Comments);
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


    }
}
