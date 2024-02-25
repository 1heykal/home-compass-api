using HomeCompassApi.BLL;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class FacilityController : Controller
    {
        private readonly IRepository<Facility> _facilityRepository;
        private readonly UserRepository _userRepository;

        public FacilityController(IRepository<Facility> facilityRepository, UserRepository userRepository)
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
            return Ok(_facilityRepository.GetAll().ToList());
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

            if (_userRepository.GetById(id) is null)
                return NotFound("There is no contributor with the specified id.");

            return Ok(_facilityRepository.GetAll().Where(f => f.ContributorId == id).ToList());

        }

        [HttpGet("bycategory/{categoryId}")]
        public ActionResult<List<Facility>> GetByCategory(int categoryId)
        {
            return Ok(_facilityRepository.GetAll().Where(f => f.CategoryId == categoryId).ToList());
        }

        [HttpPut]
        public IActionResult Update(Facility facility)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_facilityRepository.GetById(facility.Id) is null)
                return NotFound($"There is no facility with the specified Id: {facility.Id}");

            _facilityRepository.Update(facility);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var facility = _facilityRepository.GetById(id);

            if (facility is null)
                return NotFound($"There is no facility with the specified Id: {id}");



            _facilityRepository.Delete(id);
            return NoContent();
        }
    }
}
