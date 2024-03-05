using HomeCompassApi.Repositories.Cases;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Cases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Cases
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
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
        public async Task<IActionResult> CreateAsync(Missing missing)
        {
            if (!await _userRepository.IsExisted(missing.ReporterId))
                return NotFound($"There is no reporter with the specified id: {missing.ReporterId}");

            await _missingRepository.Add(missing);

            return CreatedAtAction(nameof(Get), new { Id = missing.Id }, missing);
        }

        [HttpGet]
        public async Task<ActionResult<List<MissingDTO>>> GetAllAsync()
        {
            return Ok(await _missingRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Missing>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var missing = await _missingRepository.GetById(id);

            if (missing is null)
                return NotFound($"There is no record with the specified Id: {id}");

            return Ok(missing);
        }

        [HttpPost("page")]
        public async Task<ActionResult<List<Missing>>> GetByPageAsync([FromBody] PageDTO page)
        {
            return Ok(await _missingRepository.GetByPageAsync(page));
        }

        [HttpGet("reporter/{id}")]
        public async Task<ActionResult<List<MissingDTO>>> GetByReporterIdAsync(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (!await _userRepository.IsExisted(id))
                return NotFound($"There is no reporter with the specified id: {id}");

            return Ok(await _missingRepository.GetByReporterId(id));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Missing missing)
        {
            missing.Id = id;
            if (!await _missingRepository.IsExisted(missing))
                return NotFound($"There is no record with the specified Id: {id}");

            await _missingRepository.Update(missing);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _missingRepository.IsExisted(id))
                return NotFound($"There is no record with the specified Id: {id}");

            await _missingRepository.Delete(id);
            return NoContent();
        }
    }
}
