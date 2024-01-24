using HomeCompassApi.BLL;
using HomeCompassApi.Models.Feed;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {
        private readonly IRepository<Post, Guid> _repository;

        public PostController(IRepository<Post, Guid> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Post>> Get() => _repository.GetAll().ToList();

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (post is null)
                return BadRequest();

            _repository.Add(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpGet("{id}")]
        public ActionResult<Post> Get(Guid id)
        {
            var post = _repository.GetById(id);

            if (post is null)
                return NotFound(id);

            return post;
        }

        [HttpPost]
        public IActionResult Update(Post post)
        {
            if (post is null || _repository.GetById(post.Id) is null)
                return BadRequest();

            _repository.Update(post);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var post = _repository.GetById(id);
            if (post is null)
            {
                return NotFound(id);
            }

            _repository.Delete(id);
            return NoContent();
        }
    }
}
