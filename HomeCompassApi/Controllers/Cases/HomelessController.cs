﻿using HomeCompassApi.BLL;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeCompassApi.Controllers.Cases
{
    [ApiController]
    [Route("[controller]")]

    public class HomelessController : Controller
    {
        private readonly IRepository<Homeless> _homelessRepository;
        private readonly UserRepository _userRepository;
        public HomelessController(IRepository<Homeless> homelessRepository, UserRepository userRepository)
        {
            _homelessRepository = homelessRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<List<Homeless>> Get()
        {
            return Ok(_homelessRepository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Homeless> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var homeless = _homelessRepository.GetById(id);

            if (homeless is null)
                return NotFound($"There is no record with the specified Id: {id}");

            return Ok(homeless);
        }

        [HttpGet("reporter/{id}")]
        public ActionResult<List<Homeless>> GetByReporterId(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (_userRepository.GetById(id) is null)
                return NotFound("There is no reporter with the specified id.");

            return Ok(_homelessRepository.GetAll().Where(h => h.ReporterId == id).ToList());
        }

        [HttpPost]
        public IActionResult Create(Homeless homeless)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // if (homeless.ReporterId)

            _homelessRepository.Add(homeless);
            return CreatedAtAction(nameof(Get), new { id = homeless.Id }, homeless);
        }

        [HttpPut]
        public IActionResult Update(Homeless homeless)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_homelessRepository.GetById(homeless.Id) is null)
                return NotFound($"There is no record with the specified Id: {homeless.Id}");

            _homelessRepository.Update(homeless);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var homeless = _homelessRepository.GetById(id);

            if (homeless is null)
                return NotFound($"There is no record with the specified Id: {id}");

            _homelessRepository.Delete(id);

            return NoContent();
        }


    }
}
