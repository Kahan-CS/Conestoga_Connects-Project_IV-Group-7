using Microsoft.AspNetCore.Mvc;
using BlazorApp.Shared;
using BlazorApp.Server.Models;
using BlazorApp.Server.Services;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly Logger logger;

        public ContactsController(UserService userService)
        {
            _userService = userService;
            logger = new Logger();
        }

        [HttpGet("{username}")]
        public IActionResult GetUserContacts(string username)
        {
            logger.Log($"CLIENT: Requesting contacts for user {username}");
            var contacts = _userService.GetUserContacts(username);
            List<ContactModel> contactModels = new List<ContactModel>();
            foreach (var user in contacts)
            {
                contactModels.Add(new ContactModel
                {
                    Username = user.Username,
                    Name = user.Name,
                    ImageUrl = user.ProfilePictureUrl,
                    IntroText = ""

                });
            }
            logger.Log($"SERVER: Sending contacts for user {username}");
            return Ok(contactModels);
        }

        [HttpPost("{username}")] 
        public IActionResult AddContact(string username, [FromBody] ContactModel contact)
        {
            logger.Log($"CLIENT: Adding contact for user {username}");
            _userService.AddContact(username, new Contact
            {
                Id = Guid.NewGuid().ToString(),
                Username = contact.Username,
                Name = contact.Name,
                ProfilePictureUrl = contact.ImageUrl
            });
            logger.Log($"SERVER: Contact added successfully for user {username}");
            return Ok();
        }
    }
}
