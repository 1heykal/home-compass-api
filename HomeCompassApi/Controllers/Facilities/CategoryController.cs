using HomeCompassApi.BLL;
using HomeCompassApi.Models.Facilities;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Facilities
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _repository;

        public CategoryController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(category);
            return CreatedAtAction(nameof(Get), new { Id = category.Id }, category);
        }

        [HttpGet]
        public ActionResult<List<Category>> Get() => Ok(_repository.GetAll().ToList());

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = _repository.GetById(id);

            if (category is null)
                return NotFound($"There is no category with the specified Id: {category.Id}");

            return Ok(category);
        }

        [HttpPut]
        public IActionResult Update(Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_repository.GetById(category.Id) is null)
                return NotFound($"There is no category with the specified Id: {category.Id}");

            _repository.Update(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (_repository.GetById(id) is null)
                return NotFound($"There is no category with the specified Id: {id}");

            _repository.Delete(id);
            return NoContent();
        }
    }
}
