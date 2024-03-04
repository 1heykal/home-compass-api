using HomeCompassApi.BLL;
using HomeCompassApi.Models.Facilities;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Feed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportRepository _reportRepository;
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;

        public ReportsController(ReportRepository reportRepository, UserRepository userRepository, PostRepository postRepository)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Report>>> GetAsync()
        {
            return Ok(await _reportRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> Get(int id)
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
            return Ok(await _reportRepository.GetByPageAsync(page));
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Report report)
        {
            if (!await _userRepository.IsExisted(report.ReporterId))
                return NotFound($"There is no user with the specified id: {report.ReporterId}");

            if (!await _postRepository.IsExisted(report.PostId))
                return NotFound($"There is no post with the specified Id: {report.PostId}");

            report.Date = DateTime.Now;

            await _reportRepository.Add(report);

            return CreatedAtAction(nameof(Get), new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateReportDTO report)
        {

            if (id <= 0)
                return BadRequest("Id must be greater than Zero.");

            if (!await _reportRepository.IsExisted(id))
                return NotFound($"There is no report with the specified id: {id}");

            var entity = new Report
            {
                Id = id,
                Type = report.Type,
                Details = report.Details,
                Archived = report.Archived
            };

            await _reportRepository.Update(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than Zero.");

            if (!await _reportRepository.IsExisted(id))
                return NotFound($"There is no report with the specified id: {id}");

            await _reportRepository.Delete(id);

            return NoContent();
        }

    }
}
