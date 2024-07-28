using HomeCompassApi.Models.User;
using HomeCompassApi.Repositories.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeCompassApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("details")]
        public async Task<ActionResult<UserDetailsDTO>> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id cannot be null or empty.");

            var userDetails = await _userRepository.GetUserDetails(id);

            if (userDetails is null)
                return NotFound("There is no user with the specified Id");

            return Ok(userDetails);
        }

        [HttpPut("details/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserDetailsDTO userDetailsDTO)
        {
            if (!await _userRepository.IsExisted(id))
                return NotFound($"There is no user with the specified Id: {id}");

            await _userRepository.UpdateUserDetails(id, userDetailsDTO);

            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (!await _userRepository.IsExisted(id))
        //        return NotFound($"There is no user with the specified Id: {id}");

        //    await _userRepository.Delete(id);
        //    return NoContent();
        //}

    }
}
