using HomeCompassApi.BLL;
using HomeCompassApi.BLL.Facilities;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IRepository<Resource> _resourceRepository;
        public ResourceController(IRepository<Resource> resourceRepository)
        {
            _resourceRepository = resourceRepository;

        }


        [HttpPost]
        public IActionResult Create(Resource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _resourceRepository.Add(resource);
            return CreatedAtAction(nameof(Get), new { Id = resource.Id }, resource);
        }

        [HttpGet]
        public ActionResult<List<Resource>> Get()
        {
            return Ok(_resourceRepository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Resource> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var resource = _resourceRepository.GetById(id);

            if (resource is null)
                return NotFound($"There is no resource with the specified Id: {id}");

            return Ok(resource);
        }

        [HttpPut]
        public IActionResult Update(Resource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_resourceRepository.GetById(resource.Id) is null)
                return NotFound($"There is no resource with the specified Id: {resource.Id}");

            _resourceRepository.Update(resource);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (_resourceRepository.GetById(id) is null)
                return NotFound($"There is no resource with the specified Id: {id}");

            _resourceRepository.Delete(id);

            return NoContent();
        }
    }
}
