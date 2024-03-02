using HomeCompassApi.BLL;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services.Facilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _categoryRepository.Add(category);
            return CreatedAtAction(nameof(Get), new { Id = category.Id }, category);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAsync()
        {
            return Ok((await _categoryRepository.GetAll()).Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = await _categoryRepository.GetById(id);

            if (category is null)
                return NotFound($"There is no category with the specified Id: {category.Id}");

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            category.Id = id;
            if (!await _categoryRepository.IsExisted(category))
                return NotFound($"There is no category with the specified Id: {id}");

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
