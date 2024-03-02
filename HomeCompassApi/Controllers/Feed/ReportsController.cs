using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Repositories.Feed;
using HomeCompassApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportRepository _reportRepository;

        public ReportsController(ReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Report>>> GetAsync()
        {
            return Ok(await _reportRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var report = await _reportRepository.GetById(id);
            if (report is null)
                return NotFound($"There is no report with the specified id: {id}");

            return Ok(report);
        }


        [HttpPost("page")]
        public async Task<ActionResult<List<Report>>> GetByPageAsync([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok((await _reportRepository.GetAll()).Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Report report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            report.Date = DateTime.Now;

            await _reportRepository.Add(report);

            return CreatedAtAction(nameof(GetAsync), new { id = report.Id }, report);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Report report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (report.Id <= 0)
                return BadRequest("Id must be greater than Zero.");

            if (!await _reportRepository.IsExisted(report))
                return NotFound($"There is no report with the specified id: {report.Id}");

            await _reportRepository.Update(report);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than Zero.");

            if (!await _reportRepository.IsExisted(new Report { Id = id }))
                return NotFound($"There is no report with the specified id: {id}");

            await _reportRepository.Delete(id);

            return NoContent();
        }

    }
}
