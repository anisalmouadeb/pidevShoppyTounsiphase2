using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Shelf
    {
        public long shelfId { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Expiration Date")]
        public object dateExpiration { get; set; }
        [Required(ErrorMessage = "Shelf Type is required")]
        [Display(Name = "Shelf Type")]
        public ShelfType type { get; set; }
        public string image { get; set; }
        public List<Category> category { get; set; }
        public double rating { get; set; }
        public bool promo { get; set; }
        [Range(0, 100, ErrorMessage = "Reduction Percentage Must be between 0 and 100")]
        [Display(Name = "reduction Percantage ")]
        public int reductionPercantage { get; set; }
        [Required(ErrorMessage = "Shelf Name is required")]
        [Display(Name = "Shelf Name")]
        public string shelfname { get; set; }
    }
}