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
        private readonly IRepository<Post> _repository;

        public PostController(IRepository<Post> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (post is null)
                return BadRequest();

            _repository.Add(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }


        [HttpGet]
        public ActionResult<List<Post>> Get() => _repository.GetAll().ToList();

        [HttpGet("{id}")]
        public ActionResult<Post> Get(int id)
        {
            var post = _repository.GetById(id);

            if (post is null)
                return NotFound(id);

            return post;
        }

        [HttpPut]
        public IActionResult Update(Post post)
        {
            if (post is null)
                return BadRequest();

            if (_repository.GetById(post.Id) is null)
                return NotFound(post);

            _repository.Update(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _repository.GetById(id);
            if (post is null)
                return NotFound(id);


            _repository.Delete(id);
            return NoContent();
        }
    }
}
