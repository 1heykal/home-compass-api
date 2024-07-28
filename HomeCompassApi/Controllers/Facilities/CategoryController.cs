using Elfie.Serialization;
using HomeCompassApi.Repositories;
using HomeCompassApi.Repositories.Facilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HomeCompassApi.Entities.Facilities;
using HomeCompassApi.Models.Facilities;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Category category)
        {
            if (await _categoryRepository.NameExists(category.Name))
                return BadRequest($"A category with the specified name exists.");

            await _categoryRepository.Add(category);
            return CreatedAtAction(nameof(Get), new { Id = category.Id }, category);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAsync()
        {
            return Ok(await _categoryRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = await _categoryRepository.GetById(id);

            if (category is null)
                return NotFound($"There is no category with the specified Id: {id}");

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Category category)
        {
            category.Id = id;
            if (!await _categoryRepository.IsExisted(category))
                return NotFound($"There is no category with the specified Id: {id}");

            if (await _categoryRepository.NameExists(id, category.Name))
                return BadRequest($"A resource with the specified name exists.");

            await _categoryRepository.Update(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (await _categoryRepository.GetById(id) is null)
                return NotFound($"There is no category with the specified Id: {id}");

            await _categoryRepository.Delete(id);
            return NoContent();
        }
    }
}