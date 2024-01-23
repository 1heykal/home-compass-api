using HomeCompassApi.BLL;
using HomeCompassApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers
{
    public class PostController : Controller
    {
        private readonly IRepository<Post, Guid> _repository;

        public PostController(IRepository<Post, Guid> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return Ok(_repository.GetAll());
        }

        public IActionResult Create()
        {
            return NotFound();
        }

        public IActionResult Get(Guid id)
        {
            return NotFound(id);
        }

        public IActionResult Edit(Post post)
        {
            return NoContent();
        }

        public IActionResult Delete(Guid id)
        {
            return NoContent();
        }
    }
}
