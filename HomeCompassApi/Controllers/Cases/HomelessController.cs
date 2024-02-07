using HomeCompassApi.BLL;
using HomeCompassApi.Models.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Cases
{
    [ApiController]
    [Route("[controller]")]

    public class HomelessController : Controller
    {
        private readonly IRepository<Homeless> _repository;
        public HomelessController(IRepository<Homeless> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Homeless>> Get()
        {
            return Ok(_repository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Homeless> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var homeless = _repository.GetById(id);
            if (homeless is null)
                return NotFound($"There is no record with the specified Id: {id}");

            return Ok(homeless);
        }

        [HttpPost]
        public IActionResult Create(Homeless homeless)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(homeless);
            return CreatedAtAction(nameof(Get), new { id = homeless.Id }, homeless);
        }

        [HttpPut]
        public IActionResult Update(Homeless homeless)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_repository.GetById(homeless.Id) is null)
                return NotFound($"There is no record with the specified Id: {homeless.Id}");

            _repository.Update(homeless);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var homeless = _repository.GetById(id);

            if (homeless is null)
                return NotFound($"There is no record with the specified Id: {id}");

            _repository.Delete(id);

            return NoContent();
        }


    }
}
