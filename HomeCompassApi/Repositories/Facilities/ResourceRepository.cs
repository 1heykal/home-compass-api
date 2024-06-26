﻿using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories.Facilities
{
    public class ResourceRepository : IRepository<Resource>
    {
        private readonly ApplicationDbContext _context;
        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Resource entity)
        {
            await _context.Resources.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Resources.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Resource>> GetAll() => await _context.Resources.AsNoTracking().ToListAsync();

        public async Task<List<ResourceDTO>> GetAllReduced() =>
            await _context.Resources.AsNoTracking().Select(resource => new ResourceDTO()
            {
                Id = resource.Id,
                Name = resource.Name,
                IsAvailable = resource.IsAvailable

            }).ToListAsync();

        private static ResourceDTO ResourceToResourceDTO(Resource resource)
        {
            return new ResourceDTO()
            {
                Id = resource.Id,
                Name = resource.Name,
                IsAvailable = resource.IsAvailable
            };
        }

        public async Task<List<ResourceDTO>> GetByPageAsync(PageDTO page)
        {
            return await _context.Resources.AsNoTracking().Select(resource => new ResourceDTO()
            {
                Id = resource.Id,
                Name = resource.Name,
                IsAvailable = resource.IsAvailable

            }).Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();
        }


        public async Task<Resource> GetById(int id) => await _context.Resources.FindAsync(id);

        public async Task<Resource> GetByName(string name) => await _context.Resources.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
        public async Task<bool> IsExisted(Resource resource) => await _context.Resources.ContainsAsync(resource);

        public async Task<bool> IsExisted(int id) => await _context.Resources.AnyAsync(e => e.Id == id);

        public async Task<bool> NameExists(int id, string name) => await _context.Resources.AnyAsync(r => r.Name.ToLower() == name.ToLower() && r.Id != id);

        public async Task<bool> NameExists(string name) => await _context.Resources.AnyAsync(r => r.Name.ToLower() == name.ToLower());
        public async Task Update(Resource entity)
        {
            _context.Resources.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
