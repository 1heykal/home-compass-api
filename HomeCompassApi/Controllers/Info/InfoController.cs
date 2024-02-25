using HomeCompassApi.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeCompassApi.Models;

namespace HomeCompassApi.Controllers.Info
{
    [Route("[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IRepository<Models.Info> _infoRepository;

        public InfoController(IRepository<Models.Info> infoRepository)
        {
            _infoRepository = infoRepository;
        }

        [HttpGet]
        public ActionResult<List<Models.Info>> Get()
        {
            return Ok(_infoRepository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Models.Info> GetById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var info = _infoRepository.GetById(id);
            if (info is null)
                return NotFound($"There is no info with the specified id: {id}");

            return Ok(info);
        }

        [HttpGet("bycategory/category")]
        public ActionResult<List<Models.Info>> GetByCategory(string category)
        {
            if (category is null || category == string.Empty)
                return BadRequest();

            var info = _infoRepository.GetAll().Where(i => i.Category.ToLower() == category.ToLower()).ToList();

            if (info is null || info.Count == 0)
                return NotFound($"There is no info with the specified category: {category}");

            return Ok(info);

        }

        [HttpPost]
        public IActionResult Create(Models.Info info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _infoRepository.Add(info);

            return CreatedAtAction(nameof(Get), new { id = info.Id }, info);
        }

        [HttpPut]
        public IActionResult Update(Models.Info entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entity.Id <= 0)
                return BadRequest("Id must be greater than Zero.");

            var info = _infoRepository.GetById(entity.Id);

            if (info is null)
                return NotFound($"There is no info with the specified id: {entity.Id}");

            _infoRepository.Update(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than Zero.");

            if (_infoRepository.GetById(id) is null)
                return NotFound($"There is no info with the specified id: {id}");

            _infoRepository.Delete(id);

            return NoContent();
        }



    }
}
