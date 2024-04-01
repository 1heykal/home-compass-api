using HomeCompassApi.Repositories.Facilities;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Facilities;
using Microsoft.AspNetCore.Mvc;
using Humanizer.Localisation;
using Org.BouncyCastle.Asn1.X509;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly FacilityRepository _facilityRepository;
        private readonly UserRepository _userRepository;
        private readonly CategoryRepository _categoryRepository;

        public FacilityController(FacilityRepository facilityRepository, UserRepository userRepository, CategoryRepository categoryRepository)
        {
            _facilityRepository = facilityRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Facility facility)
        {
            if (!await _userRepository.IsExisted(facility.ContributorId))
                return NotFound($"There is no contibutor with the specified id: {facility.ContributorId}");

            if (!await _categoryRepository.IsExisted(facility.CategoryId))
                return NotFound($"There is no category with the specified Id: {facility.CategoryId}");

            await _facilityRepository.Add(facility);

            return CreatedAtAction(nameof(Get), new { facility.Id }, facility);
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadFacilitiesDTO>>> GetAsync()
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
        public async Task<ActionResult<List<Facility>>> GetByContributorIdAsync(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (!await _userRepository.IsExisted(id))
                return NotFound($"There is no contibutor with the specified id: {id}");

            return Ok(await _facilityRepository.GetByContributorIdAsync(id));

        }

        [HttpGet("bycategory/{categoryId}")]
        public async Task<ActionResult<List<Facility>>> GetByCategoryAsync(int categoryId)
        {
            return Ok(await _facilityRepository.GetByCategoryAsync(categoryId));
        }

        [HttpPost("page")]
        public async Task<ActionResult<List<ReadFacilitiesDTO>>> GetByPageAsync([FromBody] PageDTO page)
        {
            return Ok(await _facilityRepository.GetByPageAsync(page));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateFacilityDTO facility)
        {
            if (!await _facilityRepository.IsExisted(id))
                return NotFound($"There is no facility with the specified Id: {id}");

            if (!await _categoryRepository.IsExisted(facility.CategoryId))
                return NotFound($"There is no category with the specified Id: {id}");

            var entity = UpdateFacilityDTOToFacility(await _facilityRepository.GetById(id), facility);

            await _facilityRepository.Update(entity);
            return NoContent();
        }

        private static Facility UpdateFacilityDTOToFacility(Facility facility, UpdateFacilityDTO facilityDTO)
        {

            facility.Name = facilityDTO.Name;
            facility.Location = facilityDTO.Location;
            facility.Description = facilityDTO.Description;
            facility.CategoryId = facilityDTO.CategoryId;
            facility.Days = facilityDTO.Days;
            facility.Resources = facilityDTO.Resources;
            facility.Hours = facilityDTO.Hours;
            facility.Target = facilityDTO.Target;
            facility.PhotoUrl = facilityDTO.PhotoUrl;
            facility.Description = facilityDTO.Description;

            return facility;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _facilityRepository.IsExisted(id))
                return NotFound($"There is no facility with the specified Id: {id}");

            await _facilityRepository.Delete(id);
            return NoContent();
        }
    }
}
