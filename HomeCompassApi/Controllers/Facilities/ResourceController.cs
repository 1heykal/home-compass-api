using HomeCompassApi.BLL;
using HomeCompassApi.BLL.Facilities;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
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
            return Ok(_resourceRepository.GetAll());
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

        [HttpPost("page")]
        public ActionResult<List<Resource>> GetByPage([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok(_resourceRepository.GetAll().Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPut]
        public IActionResult Update(Resource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_resourceRepository.IsExisted(resource))
                return NotFound($"There is no resource with the specified Id: {resource.Id}");

            _resourceRepository.Update(resource);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!_resourceRepository.IsExisted(new Resource { Id = id }))
                return NotFound($"There is no resource with the specified Id: {id}");

            _resourceRepository.Delete(id);

            return NoContent();
        }
    }
}
