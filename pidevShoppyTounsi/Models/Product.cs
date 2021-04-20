using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Product
    {
        

        public long productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [Display(Name = "Purchase Price")]
        public double priceV { get; set; }
        public double priceA { get; set; }
        public int quantity { get; set; }
        public int code { get; set; }
        public bool inPromo { get; set; }
        public string image { get; set; }
        public object ad { get; set; }
        public List<object> claim { get; set; }
    }
}