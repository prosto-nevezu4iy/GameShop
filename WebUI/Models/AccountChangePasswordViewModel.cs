using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class AccountChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Create Password")]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }
    }
}