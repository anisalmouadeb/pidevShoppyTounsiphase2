using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models.Login
{
    public class ForgetPassword 
    {
          [Required]
        public String username { get; set; }
    }
}