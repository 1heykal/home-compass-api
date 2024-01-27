using HomeCompassApi.BLL;
using HomeCompassApi.Models.Cases;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Cases
{
    [ApiController]
    [Route("[controller]")]
    public class MissingController : Controller
    {
        private readonly IRepository<Missing> _repository;

        public MissingController(IRepository<Missing> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create(Missing missing)
        {
            if (missing is null)
                return BadRequest();

            _repository.Add(missing);

            // Know more about the following sentence it has no id basically
            // Why get in nameof(get)
            return CreatedAtAction(nameof(Get), new { Id = missing.Id }, missing);
        }

        [HttpGet]
        public ActionResult<List<Missing>> Get()
        {
            return _repository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Missing> Get(int id)
        {
            var missing = _repository.GetById(id);

            if (id <= 0)
                return BadRequest();

            if (missing is null)
                return NotFound();

            return missing;
        }


        [HttpPut]
        public IActionResult Update(Missing missing)
        {
            if (missing is null || missing.Id <= 0)
                return BadRequest();

            if (_repository.GetById(missing.Id) is null)
                return NotFound();

            _repository.Update(missing);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var missing = _repository.GetById(id);
            if (missing is null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
