using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Cases;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
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

            if (_userRepository.GetById(id) is null)
                return NotFound("There is no user with the specified id.");

            return Ok(_postRepository.GetAll().Where(p => p.UserId == id).ToList());

        }



        [HttpPut]
        public IActionResult Update(Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_postRepository.GetById(post.Id) is null)
                return NotFound($"There is no post with the specified Id: {post.Id}");

            _postRepository.Update(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var post = _postRepository.GetById(id);

            if (post is null)
                return NotFound($"There is no post with the specified Id: {id}");


            _postRepository.Delete(id);
            return NoContent();
        }
    }
}
