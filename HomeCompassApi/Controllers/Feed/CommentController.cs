﻿using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using HomeCompassApi.Models.Feed;
using HomeCompassApi.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Feed
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly UserRepository _userRepository;
        private readonly IRepository<Post> _postRepository;

        public CommentController(IRepository<Comment> commentRepository, UserRepository userRepository, IRepository<Post> postRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
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

        [HttpGet("post/{id}")]
        public ActionResult<List<Comment>> GetByPostId(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid post id");

            if (!_postRepository.IsExisted(new Post { Id = id }))
                return NotFound($"There is no post with the specified Id: {id}");

            return Ok(_commentRepository.GetAll().Where(c => c.PostId == id).ToList());
        }


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

            if (!_userRepository.IsExisted(new ApplicationUser { Id = id }))
                return NotFound($"There is no user with the specified id: {id}");

            return Ok(_commentRepository.GetAll().Where(c => c.UserId == id).ToList());

        }

        [HttpGet("post/{postId}/page/{page}/size/{pageSize}")]
        public ActionResult<List<Comment>> GetByPage(int postId, int page, int pageSize)
        {
            if (page < 0 || pageSize <= 0)
                return BadRequest();

            return Ok(_commentRepository.GetAll().Where(c => c.PostId == postId).Skip(page).Take(pageSize).ToList());
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (!_commentRepository.IsExisted(new Comment { Id = id }))
                return NotFound($"There is no comment with the specified Id: {id}");

            _commentRepository.Delete(id);
            return NoContent();
        }

        [HttpPut] // ("{id}")
        public IActionResult Update(Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_commentRepository.IsExisted(comment))
                return NotFound($"There is no comment with the specified Id: {comment.Id}");

            _commentRepository.Update(comment);
            return NoContent();

        }

    }
}
