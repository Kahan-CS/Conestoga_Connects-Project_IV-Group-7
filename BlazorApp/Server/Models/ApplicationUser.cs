using Microsoft.AspNetCore.Identity;

namespace BlazorApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePictureUrl { get; set; } = "https://www.pngarts.com/files/10/Default-Profile-Picture-PNG-Free-Download.png";
        public string password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
