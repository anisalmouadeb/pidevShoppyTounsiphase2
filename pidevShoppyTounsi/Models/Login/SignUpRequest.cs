using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace pidevShoppyTounsi.Models.Login
{
    public class SignUpRequest
    {
    [Required(ErrorMessage ="Name is required")]
    [MinLength(3,ErrorMessage ="Name must be between 3 and 20 letters")]
		[MaxLength(20, ErrorMessage = "Name must be between 3 and 20 letters")]
		[Display(Name ="Name")]
		public String name{get;set;}

		[Required(ErrorMessage = "Email is required")]
		[MaxLength( 50, ErrorMessage = "Maximum letters 50")]
		[DataType(DataType.EmailAddress,ErrorMessage ="Please enter a valid email")]
		[Display(Name = "Email")]
		public String email{get;set;}

		[Required(ErrorMessage = "Password is required")]
		[MinLength(6, ErrorMessage = "Password must be between 6 and 40 letters")]
		[MaxLength(40, ErrorMessage = "Password must be between 6 and 40 letters")]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public String password{get;set;}
		[Required(ErrorMessage = "Age is required")]

		[Display(Name = "Age")]
		public int age{get;set;}
		[Required(ErrorMessage = "CIN is Required")]
		[Range(10000000, 99999999,
		ErrorMessage = "The size of CIN must be 8")]
		[Display(Name = "CIN")]
		public long cin{get;set;}
		[Required(ErrorMessage = "Address is required")]
		[Display(Name = "Address")]
		public String address{get;set;}
		[Required(ErrorMessage = "Sex is required")]
		[Display(Name = "Sex")]
		public String sex{get;set;}
		[Required(ErrorMessage = "Phone Number is required")]
		[Display(Name = "Phone Number")]
		[MinLength(12, ErrorMessage = "Phone Number must be 12 chiffres ")]
		[MaxLength(12, ErrorMessage = "Phone Number must be 12 chiffres ")]
		public String numTel{get;set;}

	}
}