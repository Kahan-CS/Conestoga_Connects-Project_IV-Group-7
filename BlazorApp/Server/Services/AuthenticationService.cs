using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BlazorApp.Server.Services
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginFormModel model)
        {
            if (ModelState.IsValid)
            {
                string username = model.Username;
                string password = model.Password;


                var user = _userService.getUserByUsernameAndPassword(username, password);
                if (user != null)
                {
                    // Authentication successful
                    return Ok(new { Message = "Login successful" });
                }
            }

            // Authentication failed or invalid request
            return Unauthorized(new { Message = "Invalid username or password" });
        }


    }

}
