using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models.Login
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Userame is required")]
        public String username { get; set; }
        [Required(ErrorMessage = "Password is required")]
 
        [DataType(DataType.Password)]
        public String password { get; set; }
    }

    public class JwtResponse
    {
        public long id { get; set; }
        public String username { get; set; }
        public String email { get; set; }
        public List<String> roles { get; set; }
        public String accessToken { get; set; }
        public String tokenType { get; set; } 
        public String message { get; set; }
    }
}