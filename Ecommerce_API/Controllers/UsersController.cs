using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
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
                if(user == null)
                {
                    return BadRequest(new ApiResponse(400, "Error registeration user !!"));
                }
                else
                {
                    return Ok(new ApiResponse(201, result:user));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiValidationResponse(new List<string>() { ex.Message, "an error occurred while processing your request" }));
            }
        }
    }
}
