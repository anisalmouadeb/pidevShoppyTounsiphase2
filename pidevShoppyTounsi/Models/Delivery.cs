using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Delivery
    {
        public long deliveryId { get; set; }

        public DateTime deliveryDate { get; set; }

        public int fees { get; set; }

        public DeliveryMan deliveryMan { get; set; }

        public Order order { get; set; }

        public long longitude { get; set; }

        public long latitude { get; set; }
    }
}