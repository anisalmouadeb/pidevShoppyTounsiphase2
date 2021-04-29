using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Claim
    {
     


        public long Id { get; set; }

        public string description { get; set; }
        public string decision { get; set; }

        public string etat { get; set; }
    }
}