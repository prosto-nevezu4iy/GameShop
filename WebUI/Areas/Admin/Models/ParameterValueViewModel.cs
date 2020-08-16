using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Admin.Models
{
    public class ParameterValueViewModel
    {
        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        public SelectList ParameterSubGroups { get; set; }

        [Display(Name = "Parameter SubGroup")]
        public int SubGroupId { get; set; }
    }
}