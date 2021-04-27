using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class DiscountToken
    {
        public int discountTokenId { get; set; }
        public int value { get; set; }
        public string validity { get; set; }
        public bool used { get; set; }
    }
}