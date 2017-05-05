using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Models
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Username is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]        
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage ="Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}