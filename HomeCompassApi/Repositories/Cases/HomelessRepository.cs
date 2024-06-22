using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Cases.Homeless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeCompassApi.Repositories.Cases
{
    public class HomelessRepository : IRepository<Homeless>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HomelessRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Add(Homeless entity)
        {
            await _context.Homeless.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Homeless.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Homeless>> GetAll() => await _context.Homeless.AsNoTracking().ToListAsync();

        public async Task<List<HomelessDTO>> GetAllReduced()
        {
            return await _context.Homeless.ProjectTo<HomelessDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<HomelessDTO>> GetByPageAsync(PageDTO page) =>
            await _context.Homeless.ProjectTo<HomelessDTO>(_mapper.ConfigurationProvider)
                .Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();

        public async Task<List<HomelessDTO>> GetByReporterId(string id)
        {
            return await _context.Homeless.Where(homeless => homeless.ReporterId == id)
                .ProjectTo<HomelessDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

      
        public async Task<Homeless> GetById(int id) => await _context.Homeless.FindAsync(id);

        public async Task<bool> IsExisted(Homeless homeless) => await _context.Homeless.ContainsAsync(homeless);

        public async Task<bool> IsExisted(int id) => await _context.Homeless.AnyAsync(e => e.Id == id);

        public async Task Update(Homeless entity)
        {
            _context.Homeless.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
