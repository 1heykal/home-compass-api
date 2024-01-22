using HomeCompassApi.Models;

namespace HomeCompassApi.BLL
{
    public class PostRepository : IRepository<Post, Guid>
    {
        public void Create(Post entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public Post GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Post entity)
        {
            throw new NotImplementedException();
        }
    }
}
