﻿using HomeCompassApi.Repositories;
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

        public async Task<UserDetailsDTO> GetUserDetails(string id)
        {
            return await _context.Users.Select(u => new UserDetailsDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Age = u.Age,
                Email = u.Email,
                Gender = u.Gender,
                PhotoURL = u.PhotoUrl

            }).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserDetails(ApplicationUser userDetailsDTO)
        {
            _context.Users.Update(userDetailsDTO);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(ApplicationUser user) => await _context.Users.ContainsAsync(user);

        public async Task<bool> IsExisted(string id) => await _context.Users.AnyAsync(e => e.Id == id);






    }
}
