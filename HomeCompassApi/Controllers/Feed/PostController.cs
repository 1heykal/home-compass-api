using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.CRUD;
using HomeCompassApi.Services.Feed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HomeCompassApi.Controllers.Feed
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;
        private readonly ReportRepository _reportRepository;

        public PostController(PostRepository postRepository, UserRepository userRepository, ReportRepository reportRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _reportRepository = reportRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _postRepository.Add(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }


        [HttpGet]
        public async Task<ActionResult<List<ReadAllPostsDTO>>> GetAllAsync()
        {
            return Ok(await _postRepository.GetAllReduced());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadPostDTO>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var post = await _postRepository.GetById(id);

            if (post is null)
                return NotFound($"There is no post with the specified Id: {id}");

            return Ok(post);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<ReadAllPostsDTO>>> GetByUserIdAsync(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (!await _userRepository.IsExisted(id))
                return NotFound($"There is no user with the specified id: {id}");

            return Ok((await _postRepository.GetAll()).Where(p => p.UserId == id).ToList());

        }

        [HttpPost("page")]
        public async Task<ActionResult<List<ReadAllPostsDTO>>> GetByPageAsync([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok((await _postRepository.GetAllReduced()).Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdatePostDTO post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new Post(post);
            entity.Id = id;
            if (!await _postRepository.IsExisted(entity))
                return NotFound($"There is no post with the specified Id: {id}");

            await _postRepository.Update(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _postRepository.IsExisted(id ))
                return NotFound($"There is no post with the specified Id: {id}");


        await _postRepository.Delete(id);
            return NoContent();
    }

    [HttpPost("report")]
    public async Task<IActionResult> ReportAsync([FromBody] Report report)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _postRepository.IsExisted(report.Post))
            return NotFound($"There is no post with the specified Id: {report.PostId}");

        report.Date = DateTime.Now;
        await _reportRepository.Add(report);

        return Created();
    }
}
}
