using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class DeliveryMan
    {
        public long deliveryManId { get; set; }

        public Boolean isAvailable { get; set; }

        public long longitude { get; set; }

        public long latitude { get; set; }
    }
}