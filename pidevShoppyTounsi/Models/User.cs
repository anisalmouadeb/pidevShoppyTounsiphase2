using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class User
    {
        public long userId { get; set; }
        [Required]
        public long cin { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string name { get; set; }
        [Required]
        [Display(Name = " Address")]
        public string address { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "maximum letters 50")]
        [DataType(DataType.EmailAddress)]
      
        [Display(Name = " Email")]
        public string email { get; set; }
        [Required]
        [Display(Name = " Phone number")]
         
        public string numTel { get; set; }
        [Required] 
        public string sex { get; set; }
        
        public int counterLogin { get; set; }
        [Display(Name = " Active")]
        public bool desactivate { get; set; }
        [Display(Name = "last Login Date")]
        [DataType(DataType.DateTime)]
        public DateTime lastLoginDate { get; set; }
        [Display(Name = "Date Creation")]
        [DataType(DataType.DateTime)]
        public DateTime dateCreate { get; set; }
        [Display(Name = "Points")]
        public int point { get; set; }
        public int lastyearaddpoint { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "password must be between 6 and 40 letters")]
        [MaxLength(40, ErrorMessage = "password must be between 6 and 40 letters")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [Display(Name = "Age")]
        public int age { get; set; }
        public int codeVerif { get; set; }
        [Display(Name = "Verified")]
        public bool verified { get; set; }
        public List<Role> roles { get; set; }
        public List<object> claim { get; set; }
        public List<object> @event { get; set; }
        public Shoppingcart shoppingcart { get; set; }
        [Display(Name = "Connected")]
        public bool connected { get; set; }

    }
}