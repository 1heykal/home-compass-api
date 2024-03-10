using HomeCompassApi.Models;
using HomeCompassApi.Models.Auth;
using HomeCompassApi.Services.Auth;
using HomeCompassApi.Services.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security;
using System.Security.Cryptography;

namespace HomeCompassApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;
        private readonly ApplicationDbContext _context;


        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager, EmailService emailService, ApplicationDbContext context)
        {
            _authService = authService;
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            var user = await _userManager.FindByEmailAsync(result.Email);

            var token = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            user.EmailVerificationToken = token;
            user.EmailVerificationTokenExpiresAt = DateTime.UtcNow.AddMinutes(15);
            await _context.SaveChangesAsync();
            var subject = "Home Compass App Email Confirmation";
            await _emailService.SendVerificationToken(subject, token, user.FirstName + " " + user.LastName, result.Email);


            return Ok(result);
        }

        [HttpPost("resendConfirmationEmail")]
        public async Task<IActionResult> ResendEmailConfirmation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return NotFound("There is no user with the specified email.");

            var token = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            user.EmailVerificationToken = token;
            user.EmailVerificationTokenExpiresAt = DateTime.UtcNow.AddMinutes(15);
            await _context.SaveChangesAsync();
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
                user.EmailConfirmed = true;
                user.EmailVerificationTokenExpiresAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

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

            var token = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddMinutes(15);
            user.PasswordTokenConfirmed = false;
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
                user.PasswordTokenConfirmed = true;
                user.PasswordResetTokenExpiresAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

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
            user.PasswordTokenConfirmed = false;
            await _context.SaveChangesAsync();

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
            await _context.SaveChangesAsync();

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
