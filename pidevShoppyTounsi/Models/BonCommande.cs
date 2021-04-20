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
		public String productName { get; set; }
		[Display(Name = "Provier Name")]
		public String providerName { get; set; }
		[Display(Name = "Purchase Order Date")]
		[DataType(DataType.DateTime)]
		public DateTime bonCommandeDate { get; set; }
		public int quantity { get; set; }
	}
}