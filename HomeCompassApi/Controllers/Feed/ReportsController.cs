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
        public ActionResult<List<Report>> Get()
        {
            return Ok(_reportRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Report> GetById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var report = _reportRepository.GetById(id);
            if (report is null)
                return NotFound($"There is no report with the specified id: {id}");

            return Ok(report);
        }


        [HttpPost("page")]
        public ActionResult<List<Report>> GetByPage([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok(_reportRepository.GetAll().Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPost]
        public IActionResult Create(Report report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            report.Date = DateTime.Now;

            _reportRepository.Add(report);

            return CreatedAtAction(nameof(Get), new { id = report.Id }, report);
        }

        [HttpPut]
        public IActionResult Update(Report report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (report.Id <= 0)
                return BadRequest("Id must be greater than Zero.");

            if (!_reportRepository.IsExisted(report))
                return NotFound($"There is no report with the specified id: {report.Id}");

            _reportRepository.Update(report);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than Zero.");

            if (!_reportRepository.IsExisted(new Report { Id = id }))
                return NotFound($"There is no report with the specified id: {id}");

            _reportRepository.Delete(id);

            return NoContent();
        }

    }
}
