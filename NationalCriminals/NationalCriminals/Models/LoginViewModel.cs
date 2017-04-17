using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NationalCriminals.Helpers;

namespace NationalCriminals.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [HtmlProperties(MaxLength = 255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}