using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class Login
    {
        [Required]
        [MinLength(5)] 
        public string Username { get; set; }
        [Required]
        [MinLength(10)]
        public string Password { get; set; }
    }
}
