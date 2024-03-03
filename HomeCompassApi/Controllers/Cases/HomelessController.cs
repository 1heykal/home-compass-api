using HomeCompassApi.BLL;
using HomeCompassApi.BLL.Cases;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeCompassApi.Controllers.Cases
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class HomelessController : Controller
    {
        private readonly HomelessRepository _homelessRepository;
        private readonly UserRepository _userRepository;
        public HomelessController(HomelessRepository homelessRepository, UserRepository userRepository)
        {
            _homelessRepository = homelessRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Homeless>>> GetAsync()
        {
            return Ok(await _homelessRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Homeless>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var homeless = await _homelessRepository.GetById(id);

            if (homeless is null)
                return NotFound($"There is no record with the specified Id: {id}");

            return Ok(homeless);
        }


        [HttpPost("page")]
        public async Task<ActionResult<List<Homeless>>> GetByPageAsync([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok((await _homelessRepository.GetAllReduced()).Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }

        [HttpGet("reporter/{id}")]
        public async Task<ActionResult<List<Homeless>>> GetByReporterId(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (!await _userRepository.IsExisted(id))
                return NotFound($"There is no reporter with the specified id: {id}");

            return Ok((await _homelessRepository.GetAll()).Where(h => h.ReporterId == id).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Homeless homeless)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.IsExisted(homeless.ReporterId))
                return NotFound($"There is no reporter with the specified id: {homeless.ReporterId}");

            await _homelessRepository.Add(homeless);
            return CreatedAtAction(nameof(Get), new { id = homeless.Id }, homeless);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Homeless homeless)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            homeless.Id = id;
            if (!await _homelessRepository.IsExisted(homeless))
                return NotFound($"There is no record with the specified Id: {id}");

            await _homelessRepository.Update(homeless);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _homelessRepository.IsExisted(id))
                return NotFound($"There is no record with the specified Id: {id}");

            await _homelessRepository.Delete(id);

            return NoContent();
        }


    }
}
