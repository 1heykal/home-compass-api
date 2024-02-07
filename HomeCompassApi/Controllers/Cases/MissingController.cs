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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(missing);

            return CreatedAtAction(nameof(Get), new { Id = missing.Id }, missing);
        }

        [HttpGet]
        public ActionResult<List<Missing>> Get()
        {
            return Ok(_repository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Missing> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var missing = _repository.GetById(id);

            if (missing is null)
                return NotFound($"There is no record with the specified Id: {id}");

            return Ok(missing);
        }


        [HttpPut]
        public IActionResult Update(Missing missing)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_repository.GetById(missing.Id) is null)
                return NotFound($"There is no record with the specified Id: {missing.Id}");

            _repository.Update(missing);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var missing = _repository.GetById(id);

            if (missing is null)
                return NotFound($"There is no record with the specified Id: {id}");

            _repository.Delete(id);
            return NoContent();
        }
    }
}
