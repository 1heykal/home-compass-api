using HomeCompassApi.BLL;
using HomeCompassApi.Models.Cases;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }


        [HttpGet]
        public ActionResult<List<Post>> Get() => Ok(_repository.GetAll().ToList());

        [HttpGet("{id}")]
        public ActionResult<Post> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var post = _repository.GetById(id);

            if (post is null)
                return NotFound($"There is no post with the specified Id: {id}");

            return Ok(post);
        }

        [HttpPut]
        public IActionResult Update(Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_repository.GetById(post.Id) is null)
                return NotFound($"There is no post with the specified Id: {post.Id}");

            _repository.Update(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var post = _repository.GetById(id);

            if (post is null)
                return NotFound($"There is no post with the specified Id: {id}");


            _repository.Delete(id);
            return NoContent();
        }
    }
}
