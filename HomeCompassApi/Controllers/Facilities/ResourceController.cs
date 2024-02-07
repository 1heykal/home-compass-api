using HomeCompassApi.BLL;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Facilities;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : Controller
    {
        private readonly IRepository<Resource> _repository;

        public ResourceController(IRepository<Resource> repository)
        {
            _repository = repository;
        }


        [HttpPost]
        public IActionResult Create(Resource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(resource);
            return CreatedAtAction(nameof(Get), new { Id = resource.Id }, resource);
        }

        [HttpGet]
        public ActionResult<List<Resource>> Get()
        {
            return Ok(_repository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Resource> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var resource = _repository.GetById(id);

            if (resource is null)
                return NotFound($"There is no resource with the specified Id: {id}");

            return Ok(resource);
        }

        [HttpPut]
        public IActionResult Update(Resource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_repository.GetById(resource.Id) is null)
                return NotFound($"There is no resource with the specified Id: {resource.Id}");

            _repository.Update(resource);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (_repository.GetById(id) is null)
                return NotFound($"There is no resource with the specified Id: {id}");

            _repository.Delete(id);

            return NoContent();
        }
    }
}
