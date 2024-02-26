using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;

namespace HomeCompassApi.Repositories.User
{
    public class UserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool IsExisted(ApplicationUser user) => _context.Users.Contains(user);



    }
}
