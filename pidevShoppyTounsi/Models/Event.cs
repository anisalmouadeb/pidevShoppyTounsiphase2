using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Event
    {
        public int eventId { get; set; }
        public string name { get; set; }
        public int estimatedAmount { get; set; }
        public int collectedAmount { get; set; }
        public string location { get; set; }
        public int numberOfVisits { get; set; }
        public int numberOfparticipants { get; set; }
        public int numberOfCurrentParticipants { get; set; }
        public string eventDate { get; set; }
    }
}