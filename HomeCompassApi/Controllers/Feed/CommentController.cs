using HomeCompassApi.BLL;
using HomeCompassApi.Models.Feed;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly IRepository<Comment> _repository;

        public CommentController(IRepository<Comment> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (comment is null)
                return BadRequest();

            _repository.Add(comment);
            return CreatedAtAction(nameof(Get), new { Id = comment.Id }, comment);
        }

        [HttpGet]
        public ActionResult<List<Comment>> Get() => _repository.GetAll().ToList();


        [HttpGet("{id}")]
        public ActionResult<Comment> Get(int id) => _repository.GetById(id);


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var comment = _repository.GetById(id);
            if (comment is null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }

        [HttpPut] // ("{id}")
        public IActionResult Update(Comment comment)
        {
            if (comment is null || _repository.GetById(comment.Id) is null)
                return BadRequest();

            _repository.Update(comment);
            return NoContent();

        }

    }
}
