using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class Entry
    {
        public int entryId { get; set; }
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Qantity Must be  greater than 0")]
        public int quantity { get; set; }
        public double montant { get; set; }
        [Required(ErrorMessage = "Entry Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entry Date")]
        public string entryDate { get; set; }
        [Required(ErrorMessage = "Product is required")]
        public Product product { get; set; }
        [Required(ErrorMessage = "Provider is required")]
        public Provider provider { get; set; }
        [Required(ErrorMessage = "Artical Price is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Artical Price Must be  greater than 0")]
        [Display(Name = "Artical Price")]
        public double articalPrice { get; set; }
    }

}