using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoadTex_MVC_Project.Models
{
    public class vmMasterData
    {
        [Required(ErrorMessage = "Must enter state name")]
        public string StateName { get; set; }

        public string Regions { get; set; }
        public List<SelectListItem> RegionName { get; set; }

        public List<tblState> States { get; set; }

        public List<string> stateList;

        public tblState regionId;







    }
}