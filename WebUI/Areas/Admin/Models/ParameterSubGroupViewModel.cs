using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Admin.Models
{
    public class ParameterSubGroupViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public SelectList ParameterGroups { get; set; }

        [Display(Name = "Parameter Group")]
        public int GroupId { get; set; }
    }
}