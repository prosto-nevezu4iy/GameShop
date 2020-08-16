using Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Validators;

namespace WebUI.Areas.Admin.Models
{
    public class CategoryAddViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public SelectList Categories { get; set; }

        [Display(Name = "Parent Category")]
        public int? ParentId { get; set; }

        [ImageValidationAttribute(ErrorMessage = "File type should be jpg or png with max size of 2mb")]
        public HttpPostedFileBase Image { get; set; }
    }
}