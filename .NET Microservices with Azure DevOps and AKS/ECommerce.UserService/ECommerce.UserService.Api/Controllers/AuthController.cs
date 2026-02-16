using ECommerce.UserService.Core.Dtos;
using ECommerce.UserService.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var userRegistrationResponse = await _userService.RegisterUser(request);
            if (userRegistrationResponse is null)
            {
                return BadRequest("User registration failed.");
            }

            return Ok(userRegistrationResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var userLoginResponse = await _userService.Login(request.Email, request.Password);
            if (userLoginResponse is null || userLoginResponse.IsSuccessful == false)
            {
                return BadRequest("User login failed.");
            }

            return Ok(userLoginResponse);
        }
    }
}
