using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class OrderLine
    {
        public int orderLineId { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public bool confirmed { get; set; }
        public Shoppingcart shoppingCart { get; set; }
    }
}