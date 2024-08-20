using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Core.IRepositories.IServices;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository usersRepository;
        private readonly UserManager<LocalUser> userManager;
        private readonly IEmailService emailService;

        public UsersController(IUsersRepository usersRepository , UserManager<LocalUser> userManager , IEmailService emailService)
        {
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            try
            {
                bool uniqueEmail = usersRepository.IsUniqueUser(model.Email);
                if (!uniqueEmail)
                {
                    return BadRequest(new ApiResponse(400, "Email already exists !!"));
                }

                var user = await usersRepository.Register(model);
                if (user == null)
                {
                    return BadRequest(new ApiResponse(400, "Error registeration user !!"));
                }
                else
                {
                    return Ok(new ApiResponse(201, result: user));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiValidationResponse(new List<string>() { ex.Message, "an error occurred while processing your request" }));
            }
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await usersRepository.Login(model);
                if (user.User == null)
                {
                    return Unauthorized(new ApiValidationResponse(new List<string>() { "Email or password inCorrect" }, 401));
                }
                return Ok(user);
            }
            return BadRequest(new ApiValidationResponse(new List<string>() { "Please try to enter the email and password correctly" }, 400));
        }



        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailForUsaer(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return BadRequest(new ApiValidationResponse(new List<string> { $"This Email {email} Not Found :(" }));
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var ForgetPasswordLink = Url.Action("", "", new { token = token, email = user.Email }, Request.Scheme);
            var subject = "Reset Password Request";
            var message = $"Please Click on the Link to Reset Your Password: {ForgetPasswordLink}";

            await emailService.SendEmailAsync(user.Email, subject, message);

            return Ok(new ApiResponse(200, "Password Reset Link Has Been Sent to Your Email.. Check Your Email :)"));
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    return NotFound(new ApiResponse(404, "Email Incorrect"));
                }

                if(string.Compare(model.newPassword, model.confirmNewPassword) != 0)
                {
                    return BadRequest(new ApiResponse(400, "Password Not Match"));
                }
                if (string.IsNullOrEmpty(model.Token))
                {
                    return BadRequest(new ApiResponse(400, "Token inValid"));
                }

                var result = await userManager.ResetPasswordAsync(user, model.Token, model.newPassword);
                if (result.Succeeded)
                {
                    return Ok(new ApiResponse(200, "Resetting Successfully"));
                }
                else
                {
                    return BadRequest(new ApiResponse(400, "Error while Resetting"));
                }
            }
            return BadRequest(new ApiResponse(400, "Check Your Info"));
        }


        [HttpPost("Reset_Token")]
        public async Task<IActionResult> TokenReserPassword([FromBody] string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var token = userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(new { token = token });
        }
    }
}
