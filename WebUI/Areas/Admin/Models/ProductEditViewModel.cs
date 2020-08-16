using Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Validators;

namespace WebUI.Areas.Admin.Models
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(20, 500)]
        public int Stock { get; set; }

        [Required]
        [Range(1, 5000)]
        public decimal Price { get; set; }

        [Range(1, 100)]
        public int? Discount { get; set; }

        public SelectList Categories { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ImagesValidationAttribute(ErrorMessage = "Files type should be jpg or png with max size of 2mb")]
        public IEnumerable<HttpPostedFileBase> Images { get; set; }

        public IEnumerable<ProductImage> UploadedImages { get; set; }

        public MultiSelectList Values { get; set; }

        [Display(Name = "Values")]
        public IEnumerable<int> ValueIds { get; set; }
    }
}