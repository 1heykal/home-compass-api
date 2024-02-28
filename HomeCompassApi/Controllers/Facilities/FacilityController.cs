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
        public IActionResult Create(Facility facility)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _facilityRepository.Add(facility);

            return CreatedAtAction(nameof(Get), new { Id = facility.Id }, facility);
        }

        [HttpGet]
        public ActionResult<List<Facility>> Get()
        {
            return Ok(_facilityRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public ActionResult<Facility> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var facility = _facilityRepository.GetById(id);

            if (facility is null)
                return NotFound($"There is no facility with the specified Id: {id}");

            return Ok(facility);
        }

        [HttpGet("contributor/{id}")]
        public ActionResult<List<Post>> GetByContributorId(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (!_userRepository.IsExisted(new ApplicationUser { Id = id }))
                return NotFound($"There is no contibutor with the specified id: {id}");

            return Ok(_facilityRepository.GetAll().Where(f => f.ContributorId == id).ToList());

        }

        [HttpGet("bycategory/{categoryId}")]
        public ActionResult<List<Facility>> GetByCategory(int categoryId)
        {
            return Ok(_facilityRepository.GetAll().Where(f => f.CategoryId == categoryId).ToList());
        }

        [HttpPost("page")]
        public ActionResult<List<Facility>> GetByPage([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok(_facilityRepository.GetAllReduced().Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPut]
        public IActionResult Update(Facility facility)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_facilityRepository.IsExisted(facility))
                return NotFound($"There is no facility with the specified Id: {facility.Id}");

            _facilityRepository.Update(facility);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!_facilityRepository.IsExisted(new Facility { Id = id }))
                return NotFound($"There is no facility with the specified Id: {id}");

            _facilityRepository.Delete(id);
            return NoContent();
        }
    }
}
