using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace pidevShoppyTounsi.Models.Login
{
    public class SignUpRequest
    {
    [Required]
    [MinLength(3,ErrorMessage ="name must be between 3 and 20 letters")]
		[MaxLength(20, ErrorMessage = "name must be between 3 and 20 letters")]
		public String name{get;set;}

		[Required]
		[MaxLength( 50, ErrorMessage = "maximum letters 50")]
		[DataType(DataType.EmailAddress)]
	public String email{get;set;}

		[Required]
		[MinLength(6, ErrorMessage = "password must be between 6 and 40 letters")]
		[MaxLength(40, ErrorMessage = "password must be between 6 and 40 letters")]
		[DataType(DataType.Password)]
		public String password{get;set;}
		[Required]
		
	public int age{get;set;}
		[Required]
		
	public long cin{get;set;}
		[Required]
	public String address{get;set;}
		[Required]
	public String sex{get;set;}
		[Required]
		[MinLength(12, ErrorMessage = "num tel must be 12 chiffres ")]
		[MaxLength(12, ErrorMessage = "num tel must be 12 chiffres ")]
		public String numTel{get;set;}

	}
}