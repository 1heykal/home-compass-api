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
            if (facility is null)
                return BadRequest();

            _repository.Add(facility);

            return CreatedAtAction(nameof(Get), new { Id = facility.Id }, facility);
        }

        [HttpGet]
        public ActionResult<List<Facility>> Get()
        {
            return _repository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Facility> Get(int id)
        {
            var facility = _repository.GetById(id);
            if (facility is null)
                return NotFound();

            return facility;
        }

        //[HttpGet("/Category/{id}")]
        //public ActionResult<List<Facility>> GetByCategory(int categoryId)
        //{
        //    return _repository.GetAll().Where(f => f.CategoryId == categoryId).ToList();
        //}

        [HttpPut]
        public IActionResult Update(Facility facility)
        {
            if (facility is null)
                return BadRequest();

            if (_repository.GetById(facility.Id) is null)
                return NotFound();

            _repository.Update(facility);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var facility = _repository.GetById(id);
            if (facility is null)
                return NotFound();

            if (id <= 0)
                return BadRequest();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
