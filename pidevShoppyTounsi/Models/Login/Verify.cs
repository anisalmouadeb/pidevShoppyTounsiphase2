using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models.Login
{
    public class Verify
    {
        [Required]
        [Range(1000,9999,ErrorMessage ="the code is between 1000 and 9999")]
       public int code { get; set; }
    }
}