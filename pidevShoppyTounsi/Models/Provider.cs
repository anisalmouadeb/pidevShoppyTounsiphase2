using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
	public class Provider
	{

		public long providerId { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[Display(Name = "Provier Name")]
		public String name { get; set; }
		[Required(ErrorMessage ="Email is required")]
		[DataType(DataType.EmailAddress,ErrorMessage ="Email not valid")]
		public String email { get; set; }
		[Required(ErrorMessage = "Note is required")]
		[Range(0, 5, ErrorMessage = "Note Must be between 0 and 5")]
		public int note { get; set; }
		[Required(ErrorMessage = "deleviry Fees is required")]
		[Range(0, float.PositiveInfinity,ErrorMessage = "deleviry Fees  Must be  greater than 0")]
		[Display(Name = "Deleviry Fees")]
		public float deleviryFees { get; set; }
		[Required(ErrorMessage = "threshold Amount is required")]
		[Range(0, float.PositiveInfinity, ErrorMessage = "threshold Amount Must be  greater than 0")]
		[Display(Name = "threshold Amount")]
		public float seuilMontant { get; set; }
		[Required(ErrorMessage = "Reduction Percentage is required")]
		[Range(0, 100, ErrorMessage = "Reduction Percentage Must be between 0 and 100")]
		[Display(Name = "Reduction Percentage")]
		public int reductionPercentage { get; set; }
		public Boolean Disponibility { get; set; }
}
}