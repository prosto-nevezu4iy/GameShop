using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Validators;

namespace WebUI.Areas.Admin.Models
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public SelectList Categories { get; set; }

        [Display(Name = "Parent Category")]
        public int? ParentId { get; set; }

        [ImageValidationAttribute(ErrorMessage = "File type should be jpg or png with max size of 2mb")]
        public HttpPostedFileBase Image { get; set; }

        public bool isImageUploaded { get; set; }
    }
}