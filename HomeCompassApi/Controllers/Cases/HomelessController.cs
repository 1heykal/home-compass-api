using HomeCompassApi.BLL;
using HomeCompassApi.Models.Cases;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers.Cases
{
    public class HomelessController : Controller
    {
        private readonly IRepository<Homeless> _repository;
        public HomelessController(IRepository<Homeless> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("{id}")]
        public ActionResult<Homeless> Get(int id)
        {
            var homeless = _repository.GetById(id);
            if (homeless is null)
                return NotFound();

            return homeless;
        }


    }
}
