using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BlazorApp.Shared;
using BlazorApp.Server.Services; //Assuming you have a UserService
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly Logger _logger;

    public UserController(UserService userService, Logger logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("{username}")]
    public IActionResult GetUser(string username)
    {
        _logger.Log("CLIENT: Requesting user " + username);
        var user = _userService.GetUserByUsername(username);
        if (user == null)
        {
            _logger.Log("SERVER: User not found: " + username);
            return NotFound("User not found");
        }

        var contact = new ContactModel
        {
            Name = user.FirstName,
            Username = user.UserName,
            ImageUrl = user.ProfilePictureUrl,
            IntroText = ""
        };

        _logger.Log("SERVER: Sending user " + username);
        return Ok(contact);
    }
}

