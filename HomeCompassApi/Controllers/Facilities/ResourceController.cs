﻿using HomeCompassApi.Entities.Facilities;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Repositories.Facilities;
using HomeCompassApi.Services;
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
            return Ok(await _resourceRepository.GetByPageAsync(page));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Resource resource)
        {
            if (!await _resourceRepository.IsExisted(id))
                return NotFound($"There is no resource with the specified Id: {id}");

            if (await _resourceRepository.NameExists(id, resource.Name))
                return BadRequest($"A resource with the specified name exists.");

            resource.Id = id;
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
