using AutoMapper;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Facilities;
using Microsoft.EntityFrameworkCore;


namespace HomeCompassApi.Repositories.Facilities
{
    public class JobRepository : IRepository<Job>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public JobRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task Add(Job entity)
        {
            await _context.Jobs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Jobs.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Job>> GetAll() => await _context.Jobs.AsNoTracking().ToListAsync();

        public async Task<List<ReadJobsDTO>> GetAllReduced()
        {
            return await _context.Jobs.Select(job =>  _mapper.Map<ReadJobsDTO>(job)).ToListAsync();
        }

        public async Task<List<Job>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Jobs.Where(j => j.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Job>> GetByContributorIdAsync(string id)
        {
            return await _context.Jobs.Where(f => f.ContributorId == id).ToListAsync();
        }

        public async Task<List<Job>> GetByPageAsync(PageDTO page) => await _context.Jobs.Skip((page.Index - 1) * page.Size).Take(page.Size).ToListAsync();

        public async Task<List<Job>> GetBySkill(string skill) => await _context.Jobs.Where(j => j.Skills.Contains(skill.ToLower())).ToListAsync();

        public async Task<Job> GetById(int id) => await _context.Jobs.FindAsync(id);

        public async Task<bool> IsExisted(Job job) => await _context.Jobs.ContainsAsync(job);

        public async Task<bool> IsExisted(int id) => await _context.Jobs.AnyAsync(e => e.Id == id);
        public async Task Update(Job entity)
        {
            _context.Jobs.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
