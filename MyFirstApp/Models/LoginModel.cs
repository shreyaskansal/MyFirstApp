using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstApp.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "*Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}