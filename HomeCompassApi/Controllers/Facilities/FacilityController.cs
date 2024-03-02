using HomeCompassApi.BLL;
using HomeCompassApi.BLL.Facilities;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly FacilityRepository _facilityRepository;
        private readonly UserRepository _userRepository;

        public FacilityController(FacilityRepository facilityRepository, UserRepository userRepository)
        {
            _facilityRepository = facilityRepository;
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Facility facility)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _facilityRepository.Add(facility);

            return CreatedAtAction(nameof(Get), new { Id = facility.Id }, facility);
        }

        [HttpGet]
        public async Task<ActionResult<List<Facility>>> GetAsync()
        {
            return Ok(await _facilityRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Facility>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var facility = await _facilityRepository.GetById(id);

            if (facility is null)
                return NotFound($"There is no facility with the specified Id: {id}");

            return Ok(facility);
        }

        [HttpGet("contributor/{id}")]
        public async Task<ActionResult<List<Post>>> GetByContributorIdAsync(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (!await _userRepository.IsExisted(new ApplicationUser { Id = id }))
                return NotFound($"There is no contibutor with the specified id: {id}");

            return Ok((await _facilityRepository.GetAll()).Where(f => f.ContributorId == id).ToList());

        }

        [HttpGet("bycategory/{categoryId}")]
        public async Task<ActionResult<List<Facility>>> GetByCategoryAsync(int categoryId)
        {
            return Ok((await _facilityRepository.GetAll()).Where(f => f.CategoryId == categoryId).ToList());
        }

        [HttpPost("page")]
        public async Task<ActionResult<List<Facility>>> GetByPageAsync([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok((await _facilityRepository.GetAllReduced()).Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Facility facility)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            facility.Id = id;
            if (!await _facilityRepository.IsExisted(facility))
                return NotFound($"There is no facility with the specified Id: {id}");

            await _facilityRepository.Update(facility);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _facilityRepository.IsExisted(new Facility { Id = id }))
                return NotFound($"There is no facility with the specified Id: {id}");

            await _facilityRepository.Delete(id);
            return NoContent();
        }
    }
}
