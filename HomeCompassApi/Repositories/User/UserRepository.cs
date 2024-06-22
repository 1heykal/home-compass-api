using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeCompassApi.Repositories;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services.Cases;
using Microsoft.EntityFrameworkCore;
using HomeCompassApi.Services.User;

namespace HomeCompassApi.Repositories.User
{
    public class UserRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Delete(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

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

        public async Task<UserDetailsDTO> GetUserDetails(string id)
        {
            return await _context.Users.ProjectTo<UserDetailsDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserDetails(string id, UpdateUserDetailsDTO userDetailsDTO)
        {
            var user = await GetById(id);
            _mapper.Map(userDetailsDTO, user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(ApplicationUser user) => await _context.Users.ContainsAsync(user);

        public async Task<bool> IsExisted(string id) => await _context.Users.AnyAsync(e => e.Id == id);


        public async Task SetEmailVerificationToken(ApplicationUser user, string token)
        {
            user.EmailVerificationToken = token;
            user.EmailVerificationTokenExpiresAt = DateTime.UtcNow.AddMinutes(15);

            await _context.SaveChangesAsync();
        }


        public async Task SetPasswordResetToken(ApplicationUser user, string token)
        {
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddMinutes(15);
            user.PasswordTokenConfirmed = false;
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmEmailAsync(ApplicationUser user)
        {
            user.EmailConfirmed = true;
            user.EmailVerificationTokenExpiresAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmPasswordToken(ApplicationUser user)
        {
            user.PasswordTokenConfirmed = true;
            user.PasswordResetTokenExpiresAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task SetPasswordTokenConfirmedAsync(ApplicationUser user)
        {
            user.PasswordTokenConfirmed = false;
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();



    }
}
