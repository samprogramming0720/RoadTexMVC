using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RoadTex_MVC_Project.Models.Role_Model
{
    public class RoleModel
    {
        public List <tblRole> ListOfRoles { get; set; }
        public tblRole Role { get; set; }
        [Required]
        [Display(Name ="Role Description")]
        public string RoleDescription { get; set; }
    }
}