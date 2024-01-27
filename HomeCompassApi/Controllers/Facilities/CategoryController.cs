using HomeCompassApi.BLL;
using HomeCompassApi.Models.Facilities;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _repository;

        public CategoryController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category is null)
                return BadRequest();

            _repository.Add(category);
            return CreatedAtAction(nameof(Get), new { Id = category.Id }, category);
        }

        [HttpGet]
        public ActionResult<List<Category>> Get() => _repository.GetAll().ToList();

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = _repository.GetById(id);

            if (category is null)
                return NotFound();

            return category;
        }

        [HttpPut]
        public IActionResult Update(Category category)
        {
            if (category is null)
                return BadRequest();

            if (_repository.GetById(category.Id) is null)
                return NotFound();

            _repository.Update(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (_repository.GetById(id) is null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
