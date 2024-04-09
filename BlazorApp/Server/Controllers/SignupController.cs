using BlazorApp.Server.Models;
using BlazorApp.Server.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/Signup/")]
    [Consumes("application/json")]
    public class SignupController : ControllerBase
    {
        private readonly UserService _userService;
        public SignupController(UserService userService)
        {
            _userService = userService;
        }

        [Route("signup")]
        [HttpPost]
        public IActionResult Post([FromBody] SignupFormModel model)
        {
            logger.Log("CLIENT: Signup attempt for username: " + model.Username);

            // Validate the model using data annotations
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true))
            {
                return BadRequest(validationResults.Select(vr => vr.ErrorMessage).ToList());
            }

            // Check if the username is already taken
            if (_userService.IsUsernameTaken(model.Username))
            {
                logger.Log("SERVER: Username already taken: " + model.Username);
                return BadRequest("Username already taken");
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                password = model.Password // Make sure the property name is consistent
            };

            _userService.InsertUser(user);
            logger.Log("SERVER: Signup successful for username: " + model.Username);
            return Ok("User created successfully");
        }
    }
}

