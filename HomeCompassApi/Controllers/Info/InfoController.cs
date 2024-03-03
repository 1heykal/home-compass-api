using HomeCompassApi.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Services;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Repositories;

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
        public async Task<ActionResult<List<Models.Info>>> GetAsync()
        {
            return Ok((await _infoRepository.GetAll()).ToList());
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

            var info = (await _infoRepository.GetAll()).Where(i => i.Category.ToLower() == category.ToLower()).ToList();

            if (info is null || info.Count == 0)
                return NotFound($"There is no info with the specified category: {category}");

            return Ok(info);

        }

        [HttpPost("page")]
        public async Task<ActionResult<List<Models.Info>>> GetByPageAsync([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok((await _infoRepository.GetAll()).Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Models.Info info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _infoRepository.Add(info);

            return CreatedAtAction(nameof(Get), new { id = info.Id }, info);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Models.Info info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
