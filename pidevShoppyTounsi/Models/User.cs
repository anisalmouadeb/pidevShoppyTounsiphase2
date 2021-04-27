﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pidevShoppyTounsi.Models
{
    public class User
    {
        public long userId { get; set; }
        [Required(ErrorMessage = "Cin number is required")]
        public long cin { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage ="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Address is required")]
       
        [Display(Name = " Address")]
        public string address { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "Maximum letters 50")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Enter a valid email address")]
      
        [Display(Name = " Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = " Phone number")]
         
        public string numTel { get; set; }
        [Required(ErrorMessage = "Sex is required")]
        [Display(Name = "Sex")]
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
        [Required( ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be between 6 and 40 letters")]
        [MaxLength(40, ErrorMessage = "Password must be between 6 and 40 letters")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required(ErrorMessage = "Age is required")] 
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