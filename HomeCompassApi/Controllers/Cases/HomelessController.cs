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
            return _repository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Homeless> Get(int id)
        {
            var homeless = _repository.GetById(id);
            if (homeless is null)
                return NotFound();

            return homeless;
        }

        [HttpPost]
        public IActionResult Create(Homeless homeless)
        {
            if (homeless is null)
                return BadRequest("Please provide the Homeless...");

            if (_repository.GetById(homeless.Id) is null)
                return NotFound($"There is no record with the specified Id {homeless.Id}");

            _repository.Add(homeless);
            return CreatedAtAction(nameof(Get), new { id = homeless.Id }, homeless);
        }

        [HttpPut]
        public IActionResult Update(Homeless homeless)
        {
            if (homeless is null)
                return BadRequest();

            if (_repository.GetById(homeless.Id) is null)
                return NotFound();

            _repository.Update(homeless);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var homeless = _repository.GetById(id);
            if (homeless is null)
                return NotFound();

            _repository.Delete(id);

            return NoContent();
        }


    }
}
