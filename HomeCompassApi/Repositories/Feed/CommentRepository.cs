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

        public void Add(Comment entity)
        {
            _context.Comments.Add(entity);
            _context.SaveChanges();
        }

        public List<Comment> GetAll() => _context.Comments.AsNoTracking().ToList();

        public List<CommentDTO> GetAllReduced()
        {
            return _context.Comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Content = c.Content,
                AuthorName = $"{c.User.FirstName} {c.User.LastName}",
                AuthorPhotoURL = c.User.PhotoUrl
            }
            ).ToList();
        }

        public Comment GetById(int id) => _context.Comments.AsNoTracking().FirstOrDefault(c => c.Id == id);
        public bool IsExisted(Comment comment) => _context.Comments.Contains(comment);

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
