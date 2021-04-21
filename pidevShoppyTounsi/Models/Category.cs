using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Category
    {
        public long categoryId { get; set; }
        public string name { get; set; }
        public List<Product> product { get; set; }
        public object type { get; set; }
        public string categoryType { get; set; }
        public int lastShelf { get; set; }
        public Shelf shelf { get; set; }
    }

}