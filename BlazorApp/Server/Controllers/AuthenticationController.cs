using BlazorApp.Server.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly Logger logger;

        public AuthController(UserService userService)
        {
            _userService = userService;
            logger = new Logger();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginFormModel model)
        {
            logger.Log("CLIENT: Login attempt for username: " + model.Username);
            if (ModelState.IsValid)
            {
                string username = model.Username;
                string password = model.Password;

                var user = _userService.getUserByUsernameAndPassword(username, password);
                if (user != null)
                {
                    logger.Log("SERVER: Login successful for username: " + model.Username);
                    //Authentication successful
                    return Ok(new { Message = "Login successful" });
                }
            }

            logger.Log("SERVER: Login failed for username: " + model.Username);
            // Authentication failed or invalid request
            return Unauthorized(new { Message = "Invalid username or password" });
        }


    }

}
