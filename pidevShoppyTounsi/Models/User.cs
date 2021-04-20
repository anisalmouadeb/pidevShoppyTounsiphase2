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
        public long cin { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = " Address")]
        public string address { get; set; }
        [Display(Name = " Email")]
        public string email { get; set; }
        [Display(Name = " Phone number")]
        public string numTel { get; set; }
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
        public string password { get; set; }
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