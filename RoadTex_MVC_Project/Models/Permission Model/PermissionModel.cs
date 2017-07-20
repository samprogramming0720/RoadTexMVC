using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace RoadTex_MVC_Project.Models.Permission_Model
{
    public class PermissionModel
    {
        public List<tblPermission> ListOfPermissions { get; set; }
        public tblPermission Permission { get; set; }
        [Required]
        [Display(Name = "Permission Description")]
        public string PermissionDescription { get; set; }
    }
}