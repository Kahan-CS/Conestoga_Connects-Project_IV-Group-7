﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class SignupFormModel
    {
        [Required]
        public string ?FirstName { get; set; }
        [Required]
        public string ?LastName { get; set; }
        [Required]
        public string ?Email { get; set; }
        [Required]
        public string ?Username { get; set; }
        [Required]
        public string ?Password { get; set; }
        [Required]
        public string ?ConfirmPassword { get; set; }
    }
}