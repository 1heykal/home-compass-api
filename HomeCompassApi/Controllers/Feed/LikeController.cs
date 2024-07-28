using HomeCompassApi.Entities.Feed;
using HomeCompassApi.Models;
using HomeCompassApi.Repositories;
using HomeCompassApi.Repositories.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    [Route("[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly LikeRepository _likeRepository;
        private readonly UserRepository _userRepository;
        private readonly PostRepository _postRepository;

        public LikeController(LikeRepository likeRepository, UserRepository userRepository, PostRepository postRepository)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Like like)
        {

            if (like.UserId == string.Empty)
                return BadRequest("UserId cannot be Empty.");

            if (!await _postRepository.IsExisted(like.PostId))
                return NotFound($"There is no Post with the specified Id: {like.PostId}");

            if (!await _userRepository.IsExisted(like.UserId))
                return NotFound($"There is no User with the specified Id: {like.UserId}");

            if (await _likeRepository.IsExisted(like))
                return BadRequest("Like with the specified key already exists");


            await _likeRepository.Add(like);
            return Created("", like);
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<List<Like>>> Get(int postId) => Ok((await _likeRepository.GetByPostId(postId)));

        [HttpPost("post/{postId}/page")]
        public async Task<ActionResult<List<Like>>> GetByPageAsync(int postId, [FromBody] PageDTO page)
        {
            return Ok(await _likeRepository.GetByPageAsync(postId, page));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Like like)
        {
            if (like.UserId == string.Empty)
                return BadRequest("UserId cannot be Empty.");

            if (!await _likeRepository.IsExisted(like))
                return NotFound("There is no such a record with the specified Ids");

            await _likeRepository.Delete(like);
            return NoContent();
        }

    }
}



