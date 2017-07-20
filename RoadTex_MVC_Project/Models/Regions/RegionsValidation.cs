using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoadTex_MVC_Project.Models.Regions
{
    public class RegionsValidation
    {
        [Required(ErrorMessage = "Must enter Region name")]
        public string RegionName { get; set; }
        [Required(ErrorMessage = "Must enter Region name")]
        public string RegionDescription { get; set; }
    }
}