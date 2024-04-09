using System.ComponentModel.DataAnnotations;
namespace BlazorApp.Shared
{
    public class SignupFormModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(250,MinimumLength = 8, ErrorMessage = "Username must be longer than 8 characters.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Username cannot contain spaces.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(250, MinimumLength = 8, ErrorMessage = "Password must be longer than 8 characters.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Password cannot contain spaces.")]
        public string Password { get; set; }
    }
}
