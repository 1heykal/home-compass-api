using HomeCompassApi.BLL;
using HomeCompassApi.Models.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class FacilityController : Controller
    {
        private readonly IRepository<Facility> _repository;

        public FacilityController(IRepository<Facility> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create(Facility facility)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(facility);

            return CreatedAtAction(nameof(Get), new { Id = facility.Id }, facility);
        }

        [HttpGet]
        public ActionResult<List<Facility>> Get()
        {
            return Ok(_repository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Facility> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var facility = _repository.GetById(id);

            if (facility is null)
                return NotFound($"There is no facility with the specified Id: {id}");

            return Ok(facility);
        }

        [HttpGet("bycategory/{categoryId}")]
        public ActionResult<List<Facility>> GetByCategory(int categoryId)
        {
            return Ok(_repository.GetAll().Where(f => f.CategoryId == categoryId).ToList());
        }

        [HttpPut]
        public IActionResult Update(Facility facility)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_repository.GetById(facility.Id) is null)
                return NotFound($"There is no facility with the specified Id: {facility.Id}");

            _repository.Update(facility);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var facility = _repository.GetById(id);

            if (facility is null)
                return NotFound($"There is no facility with the specified Id: {id}");



            _repository.Delete(id);
            return NoContent();
        }
    }
}
