using HomeCompassApi.Models;
using HomeCompassApi.Models.Auth;
using HomeCompassApi.Repositories.User;
using HomeCompassApi.Services.Auth;
using HomeCompassApi.Services.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security;
using System.Security.Cryptography;

namespace HomeCompassApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;


        public AuthController(AuthService authService, UserManager<ApplicationUser> userManager, EmailService emailService, ApplicationDbContext context, UserRepository userRepository)
        {
            _authService = authService;
            _userManager = userManager;
            _emailService = emailService;
            _userRepository = userRepository;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            var user = await _userManager.FindByEmailAsync(result.Email);

            var token = GenerateToken();

            await _userRepository.SetEmailVerificationToken(user, token);

            var subject = "Home Compass App Email Confirmation";
            await _emailService.SendVerificationToken(subject, token, user.FirstName + " " + user.LastName, result.Email);


            return Ok(result);
        }


        private static string GenerateToken()
        {
            return RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        }

        [HttpPost("resendConfirmationEmail")]
        public async Task<IActionResult> ResendEmailConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return NotFound("There is no user with the specified email.");

            var token = GenerateToken();

            await _userRepository.SetEmailVerificationToken(user, token);

            var subject = "Home Compass App Email Confirmation";

            await _emailService.SendVerificationToken(subject, token, user.FirstName + " " + user.LastName, email);

            return NoContent();
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return NotFound("There is no user with the specified email.");

            if (!user.EmailConfirmed && user.EmailVerificationTokenExpiresAt > DateTime.UtcNow && user.EmailVerificationToken == token)
            {
                await _userRepository.ConfirmEmailAsync(user);
                return Ok();
            }

            return Unauthorized();

        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> SendResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return NotFound("There is no user with the specified email.");

            var token = GenerateToken();

            await _userRepository.SetPasswordResetToken(user, token);

            var subject = "Home Compass App Password Reset";
            await _emailService.SendVerificationToken(subject, token, user.FirstName + " " + user.LastName, email);

            return NoContent();
        }

        [HttpPost("confirmpasswordtoken")]
        public async Task<IActionResult> ConfirmPasswordTokenAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return NotFound("There is no user with the specified email.");

            if (!user.PasswordTokenConfirmed && user.PasswordResetTokenExpiresAt > DateTime.UtcNow && user.PasswordResetToken == token)
            {
                await _userRepository.ConfirmPasswordToken(user);

                return Ok();
            }

            return Unauthorized();

        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(string email, string newpassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return NotFound("There is no user with the specified email.");
            if (!user.PasswordTokenConfirmed)
            {
                return BadRequest("Please confirm password token first");
            }
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, newpassword);

            await _userRepository.SetPasswordTokenConfirmedAsync(user);

            return Ok();
        }

        [HttpPost("changePasswordWithOldOne")]
        public async Task<IActionResult> ChangePasswordWithOldOne([FromBody] ChangePasswordOldNewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return NotFound("There is no user with the specified email.");

            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                return BadRequest("Wrong email or password.");
            }

            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            await _userRepository.SaveChangesAsync();

            return Ok();
        }





        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequestModel model)
        {
            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (!user.EmailConfirmed)
                return Unauthorized("Please confirm your email.");


            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);

        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is requird.");

            var result = await _authService.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("The token is invalid.");

            return Ok();

        }

    }
}
