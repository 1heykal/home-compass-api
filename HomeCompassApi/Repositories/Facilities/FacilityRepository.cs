﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeCompassApi.DbContexts;
using HomeCompassApi.Entities;
using HomeCompassApi.Entities.Facilities;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services;
using Microsoft.EntityFrameworkCore;


namespace HomeCompassApi.Repositories.Facilities
{
    public class FacilityRepository : IRepository<Facility>
    {
        private readonly ApplicationDbContext _context;
        private readonly ResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        public FacilityRepository(ApplicationDbContext context, ResourceRepository resourceRepository, IMapper mapper)
        {
            _context = context;
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }
        public async Task Add(Facility entity)
        {
            List<Resource> resources = new();
            foreach (Resource r in entity.Resources)
            {
                var resource = await _resourceRepository.GetByName(r.Name);
                if (resource is null)
                {
                    await _resourceRepository.Add(r);
                    resource = await _resourceRepository.GetByName(r.Name);
                }
                resources.Add(resource);
                
            }
            entity.Resources = resources;
            await _context.Facilities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var facility = await _context.Facilities
                .Include(f => f.Resources)
                .FirstOrDefaultAsync(f => f.Id == id);

            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Facility>> GetAll() => await _context.Facilities.AsNoTracking().ToListAsync();

        public async Task<List<ReadFacilitiesDTO>> GetAllReduced()
        {
            return await _context.Facilities
                .ProjectTo<ReadFacilitiesDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
        
        public async Task<List<ReadFacilitiesDTO>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Facilities.Where(f => f.CategoryId == categoryId).ProjectTo<ReadFacilitiesDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<Facility>> GetByContributorIdAsync(string id)
        {
            return await _context.Facilities.Where(f => f.ContributorId == id).ToListAsync();
        }

        public async Task<List<ReadFacilitiesDTO>> GetByPageAsync(PageDTO page) => await _context.Facilities
            .ProjectTo<ReadFacilitiesDTO>(_mapper.ConfigurationProvider)
            .Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();



        public async Task<Facility> GetById(int id) => await _context.Facilities.Include(f => f.Resources).FirstOrDefaultAsync(f => f.Id == id);

        public async Task<bool> IsExisted(Facility facility) => await _context.Facilities.ContainsAsync(facility);

        public async Task<bool> IsExisted(int id) => await _context.Facilities.AnyAsync(e => e.Id == id);

        public async Task Update(Facility entity)
        {
            List<Resource> resources = new();
            foreach (Resource r in entity.Resources)
            {
                var resource = await _resourceRepository.GetByName(r.Name);
                if (resource is null)
                {
                    await _resourceRepository.Add(r);
                    resource = await _resourceRepository.GetByName(r.Name);
                }
                resources.Add(resource);
                
            }
            entity.Resources = resources;
            _context.Facilities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
