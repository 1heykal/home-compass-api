using HomeCompassApi.BLL;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _categoryRepository.Add(category);
            return CreatedAtAction(nameof(Get), new { Id = category.Id }, category);
        }

        [HttpGet]
        public ActionResult<List<Category>> Get() => Ok(_categoryRepository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = _categoryRepository.GetById(id);

            if (category is null)
                return NotFound($"There is no category with the specified Id: {category.Id}");

            return Ok(category);
        }

        [HttpPut]
        public IActionResult Update(Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.IsExisted(category))
                return NotFound($"There is no category with the specified Id: {category.Id}");

            _categoryRepository.Update(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (_categoryRepository.GetById(id) is null)
                return NotFound($"There is no category with the specified Id: {id}");

            _categoryRepository.Delete(id);
            return NoContent();
        }
    }
}
