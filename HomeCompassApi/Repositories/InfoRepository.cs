using HomeCompassApi.BLL;
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
            return await _context.Info.AsQueryable().ToListAsync();
        }
        public async Task<List<InfoDTO>> GetAllDTO()
        {
            return await _context.Info.AsQueryable().Select(i => new InfoDTO
            {
                Id = i.Id,
                Category = i.Category,
                Content = i.Content
            }).ToListAsync();
        }

        public async Task<List<Models.Info>> GetByPageAsync(PageDTO page)
        {
            return await _context.Info.AsQueryable().Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }

        public async Task<Info> GetById(int id)
        {
            return await _context.Info.AsQueryable().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> IsExisted(Info entity)
        {
            return await _context.Info.AsQueryable().ContainsAsync(entity);
        }

        public async Task<bool> IsExisted(int id) => await _context.Info.AsQueryable().AnyAsync(e => e.Id == id);


        public async Task Update(Info entity)
        {
            _context.Info.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
