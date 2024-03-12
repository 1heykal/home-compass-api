using HomeCompassApi.Repositories.Facilities;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using Microsoft.AspNetCore.Mvc;
using HomeCompassApi.Services.Facilities;
using System.Configuration;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly JobRepository _jobRepository;
        private readonly UserRepository _userRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly FacilityRepository _facilityRepository;

        public JobController(JobRepository jobRepository, UserRepository userRepository, CategoryRepository categoryRepository, FacilityRepository facilityRepository)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _facilityRepository = facilityRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Job job)
        {
            if (!await _facilityRepository.IsExisted(job.EmployerId))
                return NotFound($"There is no employer with the specified id: {job.EmployerId}");

            if (!await _categoryRepository.IsExisted(job.CategoryId))
                return NotFound($"There is no category with the specified Id: {job.CategoryId}");

            await _jobRepository.Add(job);

            return CreatedAtAction(nameof(Get), new { job.Id }, job);
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadJobsDTO>>> GetAsync()
        {
            return Ok(await _jobRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var job = await _jobRepository.GetById(id);

            if (job is null)
                return NotFound($"There is no job with the specified Id: {id}");

            return Ok(job);
        }

        [HttpGet("employer/{id}")]
        public async Task<ActionResult<List<Job>>> GetByContributorIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _facilityRepository.IsExisted(id))
                return NotFound($"There is no employer with the specified id: {id}");

            return Ok(await _jobRepository.GetByEmployerIdAsync(id));

        }


        [HttpGet("skill")]
        public async Task<ActionResult<List<Job>>> GetBySkill(string skill)
        {
            if (string.IsNullOrEmpty(skill))
                return BadRequest("Skill cannot be null or Empty.");

            return Ok(await _jobRepository.GetBySkill(skill));
        }

        [HttpGet("bycategory/{categoryId}")]
        public async Task<ActionResult<List<Job>>> GetByCategoryAsync(int categoryId)
        {
            return Ok(await _jobRepository.GetByCategoryAsync(categoryId));
        }

        [HttpPost("page")]
        public async Task<ActionResult<List<Job>>> GetByPageAsync([FromBody] PageDTO page)
        {
            return Ok(await _jobRepository.GetByPageAsync(page));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateJobDTO job)
        {
            if (!await _jobRepository.IsExisted(id))
                return NotFound($"There is no job with the specified Id: {id}");

            if (!await _categoryRepository.IsExisted(job.CategoryId))
                return NotFound($"There is no category with the specified Id: {id}");

            var entity = UpdateJobDTOToJob(await _jobRepository.GetById(id), job);

            await _jobRepository.Update(entity);
            return NoContent();
        }

        private static Job UpdateJobDTOToJob(Job job, UpdateJobDTO jobDTO)
        {
            job.Title = jobDTO.Title;
            job.Benefits = jobDTO.Benefits;
            job.ContactInformation = jobDTO.ContactInformation;
            job.Hours = jobDTO.Hours;
            job.Days = jobDTO.Days;
            job.Location = jobDTO.Location;
            job.Skills = jobDTO.Skills;
            job.Salary = jobDTO.Salary;
            job.Description = jobDTO.Description;
            job.CategoryId = jobDTO.CategoryId;

            return job;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _jobRepository.IsExisted(id))
                return NotFound($"There is no job with the specified Id: {id}");

            await _jobRepository.Delete(id);
            return NoContent();
        }
    }
}
