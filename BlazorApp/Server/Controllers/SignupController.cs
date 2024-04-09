using BlazorApp.Server.Models;
using BlazorApp.Server.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/Signup/")]
    [Consumes("application/json")]
    public class SignupController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly Logger logger;
        public SignupController(UserService userService)
        {
            _userService = userService;
            logger = new Logger();
        }


        [Route("signup")]
        [HttpPost]
        public IActionResult Post([FromBody] SignupFormModel model)
        {
            logger.Log("CLIENT: Signup attempt for username: " + model.Username);
            if (ModelState.IsValid)
            {
                //Check if the username is already taken
                if (_userService.IsUsernameTaken(model.Username))
                {
                    logger.Log("SERVER: Username already taken: " + model.Username); 
                    return BadRequest(new { Message = "Username already taken" });
                }

                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    password = model.Password // Make sure the property name is consistent
                };

                _userService.InsertUser(user);
                logger.Log("SERVER: Signup successful for username: " + model.Username);
                return Ok(new { Message = "User created successfully" });
            }

            logger.Log("SERVER: Signup failed for username: " + model.Username);
            return BadRequest(new { Message = "Invalid input data" });
        }
    }
}
