using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
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
        private readonly IRepository<Post> _postRepository;
        private readonly UserRepository _userRepository;

        public PostController(IRepository<Post> postRepository, UserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _postRepository.Add(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }


        [HttpGet]
        public ActionResult<List<Post>> Get() => Ok(_postRepository.GetAll().ToList());

        [HttpGet("{id}")]
        public ActionResult<Post> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var post = _postRepository.GetById(id);

            if (post is null)
                return NotFound($"There is no post with the specified Id: {id}");

            return Ok(post);
        }

        [HttpGet("user/{id}")]
        public ActionResult<List<Post>> GetByUserId(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (!_userRepository.IsExisted(new ApplicationUser { Id = id }))
                return NotFound($"There is no user with the specified id: {id}");

            return Ok(_postRepository.GetAll().Where(p => p.UserId == id).ToList());

        }

        [HttpGet("page")]
        public ActionResult<List<Post>> GetByPage([FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok(_postRepository.GetAll().Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }


        [HttpPut]
        public IActionResult Update(Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postRepository.IsExisted(post))
                return NotFound($"There is no post with the specified Id: {post.Id}");

            _postRepository.Update(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!_postRepository.IsExisted(new Post { Id = id }))
                return NotFound($"There is no post with the specified Id: {id}");


            _postRepository.Delete(id);
            return NoContent();
        }
    }
}
