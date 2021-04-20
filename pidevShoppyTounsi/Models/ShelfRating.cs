using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class ShelfRating
    {
       
            public int ratingId { get; set; }
            public User user { get; set; }
            public Shelf shelf { get; set; }
            public int rating { get; set; }
        
    }
    
}