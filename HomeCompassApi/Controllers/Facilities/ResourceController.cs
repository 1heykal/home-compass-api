using HomeCompassApi.BLL;
using HomeCompassApi.BLL.Facilities;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Facilities;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly ResourceRepository _resourceRepository;
        public ResourceController(ResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;

        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Resource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _resourceRepository.NameExists(resource.Name))
                return BadRequest($"A resource with the specified name exists.");

            await _resourceRepository.Add(resource);
            return CreatedAtAction(nameof(Get), new { Id = resource.Id }, resource);
        }

        [HttpGet]
        public async Task<ActionResult<List<ResourceDTO>>> GetAsync()
        {
            return Ok(await _resourceRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var resource = await _resourceRepository.GetById(id);

            if (resource is null)
                return NotFound($"There is no resource with the specified Id: {id}");

            return Ok(resource);
        }

        [HttpPost("page")]
        public async Task<ActionResult<List<ResourceDTO>>> GetByPageAsync([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok((await _resourceRepository.GetAll()).Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Resource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _resourceRepository.IsExisted(id))
                return NotFound($"There is no resource with the specified Id: {id}");

            if (await _resourceRepository.NameExists(id, resource.Name))
                return BadRequest($"A resource with the specified name exists.");

            await _resourceRepository.Update(resource);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _resourceRepository.IsExisted(id))
                return NotFound($"There is no resource with the specified Id: {id}");

            await _resourceRepository.Delete(id);

            return NoContent();
        }
    }
}
