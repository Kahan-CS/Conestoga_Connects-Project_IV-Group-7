using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class LoginFormModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
