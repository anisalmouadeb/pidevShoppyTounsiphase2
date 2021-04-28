using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class CustomShelf
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Please Select Category")]
        public Category category { get; set; }
    }
}