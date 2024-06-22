using HomeCompassApi.Repositories;
using HomeCompassApi.Models;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Feed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories
{
    public class InfoRepository : IRepository<Info>
    {
        private readonly ApplicationDbContext _context;

        public InfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Info entity)
        {
            await _context.Info.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Info.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Info>> GetAll()
        {
            return await _context.Info.ToListAsync();
        }
        public async Task<List<InfoDTO>> GetAllDTO()
        {
            return await _context.Info.Select(i => new InfoDTO
            {
                Id = i.Id,
                Category = i.Category,
                Content = i.Content
            }).ToListAsync();
        }

        public async Task<List<InfoDTO>> GetByCategoryAsync(string category)
        {
            return await _context.Info.Where(i => i.Category.ToLower() == category.ToLower()).Select(i => new InfoDTO
            {
                Id = i.Id,
                Category = i.Category,
                Content = i.Content

            }).ToListAsync();
        }

        public async Task<List<Models.Info>> GetByPageAsync(PageDTO page)
        {
            return await _context.Info.Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }

        public async Task<Info> GetById(int id)
        {
            return await _context.Info.FindAsync(id);
        }

        public async Task<bool> IsExisted(Info entity)
        {
            return await _context.Info.ContainsAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(int id) => await _context.Info.AnyAsync(e => e.Id == id);


        public async Task Update(Info entity)
        {
            _context.Info.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
