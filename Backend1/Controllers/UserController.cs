using Backend1.IService;
using Backend1.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var user = await _userService.Authenticate(request.Email, request.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var jwtSecret = _configuration["Jwt:Secret"]; // Inject IConfiguration in controller
            var token = JwtTokenHelper.GenerateJwtToken(user, jwtSecret);

            return Ok(new
            {
                message = "Login successful",
                token,
                user = new { id = user.id, name = user.name, email = user.Email }
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAllUsers());
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var existing = _userService.GetByUserName(user.name).Result;
            if (existing != null)
                return BadRequest("Username already exists");

            _userService.AddUser(user);
            return Ok(new { message = "User registered successfully" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await _userService.DeleteUser(id);
        }
    }
}
