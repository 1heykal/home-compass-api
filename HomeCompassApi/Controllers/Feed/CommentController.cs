using HomeCompassApi.BLL;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly UserRepository _userRepository;

        public CommentController(IRepository<Comment> commentRepository, UserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _commentRepository.Add(comment);
            return CreatedAtAction(nameof(Get), new { Id = comment.Id }, comment);
        }

        [HttpGet]
        public ActionResult<List<Comment>> Get() => Ok(_commentRepository.GetAll().ToList());


        [HttpGet("{id}")]
        public ActionResult<Comment> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var comment = _commentRepository.GetById(id);

            if (comment is null)
                return NotFound($"There is no comment with the specified Id: {id}");

            return Ok(comment);
        }

        [HttpGet("user/{id}")]
        public ActionResult<List<Post>> GetByUserId(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (_userRepository.GetById(id) is null)
                return NotFound("There is no user with the specified id.");

            return Ok(_commentRepository.GetAll().Where(c => c.UserId == id).ToList());

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var comment = _commentRepository.GetById(id);

            if (comment is null)
                return NotFound($"There is no comment with the specified Id: {id}");

            _commentRepository.Delete(id);
            return NoContent();
        }

        [HttpPut] // ("{id}")
        public IActionResult Update(Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_commentRepository.GetById(comment.Id) is null)
                return NotFound($"There is no record with the specified Id: {comment.Id}");

            _commentRepository.Update(comment);
            return NoContent();

        }

    }
}
