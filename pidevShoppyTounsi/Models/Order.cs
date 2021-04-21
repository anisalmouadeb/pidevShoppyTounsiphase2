using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public string orderDate { get; set; }
        public double orderAmount { get; set; }
        public object delivery { get; set; }
        public object bill { get; set; }
        public List<OrderLine> orderLine { get; set; }
        public bool confirmedPayment { get; set; }

    }
}