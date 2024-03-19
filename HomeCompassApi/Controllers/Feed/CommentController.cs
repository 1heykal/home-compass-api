using HomeCompassApi.Repositories;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using HomeCompassApi.Services.Feed;
using HomeCompassApi.Services.Feed.Comment;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository _commentRepository;
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;

        public CommentController(CommentRepository commentRepository, UserRepository userRepository, PostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Comment comment)
        {
            if (!await _userRepository.IsExisted(comment.UserId))
                return NotFound($"There is no user with the specified id: {comment.UserId}");

            if (!await _postRepository.IsExisted(comment.PostId))
                return NotFound($"There is no post with the specified Id: {comment.PostId}");

            await _commentRepository.Add(comment);
            return CreatedAtAction(nameof(Get), new { Id = comment.Id }, comment);
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> GetAsync() => Ok(await _commentRepository.GetAllReduced());

        [HttpGet("post/{id}")]
        public async Task<ActionResult<List<CommentDTO>>> GetByPostId(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid post id");

            if (!await _postRepository.IsExisted(id))
                return NotFound($"There is no post with the specified Id: {id}");

            return Ok(await _commentRepository.GetByPostId(id));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than zero.");

            var comment = await _commentRepository.GetById(id);

            if (comment is null)
                return NotFound($"There is no comment with the specified Id: {id}");

            return Ok(comment);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<Post>>> GetByUserIdAsync(string id)
        {
            if (id is null || id == string.Empty)
                return BadRequest();

            if (!await _userRepository.IsExisted(id))
                return NotFound($"There is no user with the specified id: {id}");

            return Ok((await _commentRepository.GetAll()).Where(c => c.UserId == id).ToList());

        }

        [HttpPost("post/{postId}/page")]
        public async Task<ActionResult<List<CommentDTO>>> GetByPageAsync(int postId, [FromBody] PageDTO page)
        {
            return Ok(await _commentRepository.GetByPageAsync(postId, page));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!await _commentRepository.IsExisted(id))
                return NotFound($"There is no comment with the specified Id: {id}");

            await _commentRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateCommentDTO comment)
        {
            if (!await _commentRepository.IsExisted(id))
                return NotFound($"There is no comment with the specified Id: {id}");

            var entity = await _commentRepository.GetById(id);
            entity.Content = comment.Content;


            await _commentRepository.Update(entity);
            return NoContent();

        }

    }
}
