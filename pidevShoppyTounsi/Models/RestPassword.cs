using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class RestPassword
    {
        [Required]
        public String password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String token { get; set; }
    }
}