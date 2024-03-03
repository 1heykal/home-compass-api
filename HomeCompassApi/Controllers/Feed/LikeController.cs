﻿using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.Feed;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services;
using Microsoft.AspNetCore.Http;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (like.PostId <= 0 || like.UserId == string.Empty)
                return BadRequest();

            if (!await _postRepository.IsExisted( like.PostId ))
                return NotFound($"There is no Post with the specified Id: {like.PostId}");

            if (!await _userRepository.IsExisted( like.UserId ))
                return NotFound($"There is no User with the specified Id: {like.UserId}");

            if (await _likeRepository.IsExisted(like))
                return NoContent();


            await _likeRepository.Add(like);
            return CreatedAtAction(nameof(CreateAsync), like);
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<List<Like>>> GetByPostIdAsync(int postId) => Ok((await _likeRepository.GetByPostId(postId)).ToList());

        [HttpPost("post/{postId}/page")]
        public async Task<ActionResult<List<Like>>> GetByPageAsync(int postId, [FromBody] PageDTO page)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (page.Index < 0 || page.Size <= 0)
                return BadRequest();

            return Ok((await _likeRepository.GetByPostId(postId)).Skip((page.Index - 1) * page.Size).Take(page.Size).ToList());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Like like)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (like.PostId <= 0 || like.UserId == string.Empty)
                return BadRequest();

            if (!await _likeRepository.IsExisted(like))
                return NotFound("There is no such a record with the specified Ids");

            await _likeRepository.Delete(like);
            return NoContent();
        }

    }
}



