using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebUI.Validators;

namespace WebUI.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Create Password")]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }

        [MustBeTrue(ErrorMessage = "You must accept the terms")]
        public bool Terms { get; set; }
    }
}