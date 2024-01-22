using HomeCompassApi.Models;

namespace HomeCompassApi.BLL
{
    public class CommentRepository : IRepository<Comment, Guid>
    {
        private readonly ApplicationDbContext _context;
        public void Create(Comment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
