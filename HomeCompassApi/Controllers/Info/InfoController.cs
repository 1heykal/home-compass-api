using HomeCompassApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Feed;

namespace HomeCompassApi.Controllers.Info
{
    [Route("[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly InfoRepository _infoRepository;

        public InfoController(InfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<InfoDTO>>> GetAsync()
        {
            return Ok((await _infoRepository.GetAllDTO()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Info>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var info = await _infoRepository.GetById(id);
            if (info is null)
                return NotFound($"There is no info with the specified id: {id}");

            return Ok(info);
        }

        [HttpGet("bycategory/category")]
        public async Task<ActionResult<List<Models.Info>>> GetByCategoryAsync(string category)
        {
            if (category is null || category == string.Empty)
                return BadRequest();

            var info = await _infoRepository.GetByCategoryAsync(category);

            if (info is null || info.Count == 0)
                return NotFound($"There is no info with the specified category: {category}");

            return Ok(info);

        }

        [HttpPost("page")]
        public async Task<ActionResult<List<Models.Info>>> GetByPageAsync([FromBody] PageDTO page)
        {
            return Ok(await _infoRepository.GetByPageAsync(page));
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Models.Info info)
        {
            await _infoRepository.Add(info);

            return CreatedAtAction(nameof(Get), new { id = info.Id }, info);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Models.Info info)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than Zero.");

            info.Id = id;
            if (!await _infoRepository.IsExisted(info))
                return NotFound($"There is no info with the specified id: {info.Id}");

            await _infoRepository.Update(info);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than Zero.");

            if (!await _infoRepository.IsExisted(id))
                return NotFound($"There is no info with the specified id: {id}");

            await _infoRepository.Delete(id);

            return NoContent();
        }



    }
}
