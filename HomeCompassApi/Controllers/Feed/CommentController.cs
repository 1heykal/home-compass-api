using HomeCompassApi.BLL;
using HomeCompassApi.Models.Feed;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    public class CommentController : Controller
    {
        private readonly IRepository<Comment, Guid> _repository;

        public CommentController(IRepository<Comment, Guid> repository)
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

        // [HttpGet("{postId}")]
        public ActionResult<List<Comment>> Get() => _repository.GetAll().ToList();


        public IActionResult Delete(Guid id)
        {
            var comment = _repository.GetById(id);
            if (comment is null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }

        [HttpPost] // ("{id}")
        public IActionResult Update(Comment comment)
        {
            if (comment is null || _repository.GetById(comment.Id) is null)
                return BadRequest();

            _repository.Update(comment);
            return NoContent();

        }

    }
}
