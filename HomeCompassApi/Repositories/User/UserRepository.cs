using HomeCompassApi.Repositories;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services.Cases;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories.User
{
    public class UserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> IsExisted(ApplicationUser user) => await _context.Users.ContainsAsync(user);

        public async Task<bool> IsExisted(string id) => await _context.Users.AnyAsync(e => e.Id == id);






    }
}
