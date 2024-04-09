using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BlazorApp.Shared;
using BlazorApp.Server.Services; //Assuming you have a UserService

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly Logger logger;

        public UsersController(UserService userService)
        {
            _userService = userService;
            logger = new Logger();
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            logger.Log("CLIENT: Requesting all users");
            var users = _userService.GetAllUsers(); //Get all users from your UserService
            List<ContactModel> contactModels = new List<ContactModel>();
            foreach (var user in users)
            {
                contactModels.Add(new ContactModel
                {
                    Username = user.UserName,
                    Name = user.FirstName,
                    ImageUrl = user.ProfilePictureUrl,
                    IntroText = ""

                });
            }
            logger.Log("SERVER: Sending all users");
            return Ok(contactModels);
        }
    }
}
