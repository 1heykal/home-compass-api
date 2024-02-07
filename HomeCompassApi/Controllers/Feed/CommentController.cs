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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(comment);
            return CreatedAtAction(nameof(Get), new { Id = comment.Id }, comment);
        }

        [HttpGet]
        public ActionResult<List<Comment>> Get() => Ok(_repository.GetAll().ToList());


        [HttpGet("{id}")]
        public ActionResult<Comment> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var comment = _repository.GetById(id);

            if (comment is null)
                return NotFound($"There is no comment with the specified Id: {id}");

            return Ok(comment);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var comment = _repository.GetById(id);

            if (comment is null)
                return NotFound($"There is no comment with the specified Id: {id}");

            _repository.Delete(id);
            return NoContent();
        }

        [HttpPut] // ("{id}")
        public IActionResult Update(Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_repository.GetById(comment.Id) is null)
                return NotFound($"There is no record with the specified Id: {comment.Id}");

            _repository.Update(comment);
            return NoContent();

        }

    }
}
