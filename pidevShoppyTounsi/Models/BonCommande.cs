using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class BonCommande
    {
		[Key]
		public long bonCoammandId { get; set; }
		[Display(Name = "Product Name")]
		[Required(ErrorMessage ="Please select product")]
		public String productName { get; set; }
		[Required(ErrorMessage = "Please select provider")]
		[Display(Name = "Provier Name")]
		public String providerName { get; set; }
		[Display(Name = "Purchase Order Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime bonCommandeDate { get; set; }
		public int quantity { get; set; }
	}
}