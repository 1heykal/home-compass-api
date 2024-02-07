using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
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

        public void Add(Comment entity)
        {
            _context.Comments.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetAll() => _context.Comments.AsNoTracking().ToList();

        public Comment GetById(int id) => _context.Comments.AsNoTracking().FirstOrDefault(c => c.Id == id);


        public void Update(Comment entity)
        {
            _context.Comments.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Comments.Remove(GetById(id));
            _context.SaveChanges();
        }

    }
}
