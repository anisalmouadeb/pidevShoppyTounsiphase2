using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Jackpot
    {
        public int jackpotId { get; set; }
        public string name { get; set; }
        public int currentAmount { get; set; }
        public int globalAmount { get; set; }
    }
}