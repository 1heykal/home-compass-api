using HomeCompassApi.BLL;
using HomeCompassApi.BLL.Cases;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Cases
{
    [ApiController]
    [Route("[controller]")]
    public class MissingController : Controller
    {
        private readonly MissingRepository _missingRepository;
        private readonly UserRepository _userRepository;

        public MissingController(MissingRepository missingRepository, UserRepository userRepository)
        {
            _missingRepository = missingRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Create(Missing missing)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _missingRepository.Add(missing);

            return CreatedAtAction(nameof(Get), new { Id = missing.Id }, missing);
        }

        [HttpGet]
        public ActionResult<List<Missing>> Get()
        {
            return Ok(_missingRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public ActionResult<Missing> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var missing = _missingRepository.GetById(id);

            if (missing is null)
                return NotFound($"There is no record with the specified Id: {id}");

            return Ok(missing);
        }

        [HttpPost("page")]
        public ActionResult<List<Missing>> GetByPage([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok(_missingRepository.GetAllReduced().Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }

        [HttpGet("reporter/{id}")]
        public ActionResult<List<Missing>> GetByReporterId(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (_userRepository.GetById(id) is null)
                return NotFound("There is no reporter with the specified id.");

            return Ok(_missingRepository.GetAll().Where(m => m.ReporterId == id).ToList());
        }


        [HttpPut]
        public IActionResult Update(Missing missing)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_missingRepository.GetById(missing.Id) is null)
                return NotFound($"There is no record with the specified Id: {missing.Id}");

            _missingRepository.Update(missing);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var missing = _missingRepository.GetById(id);

            if (missing is null)
                return NotFound($"There is no record with the specified Id: {id}");

            _missingRepository.Delete(id);
            return NoContent();
        }
    }
}
