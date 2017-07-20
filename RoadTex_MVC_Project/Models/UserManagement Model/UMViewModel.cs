using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using RoadTex_MVC_Project.Models;

namespace RoadTex_MVC_Project.Models.UserManagement_Model
{
    public class UMViewModel
    {
        public UserInfoModel Userinfo { get; set; }
        public SelectList RolesList { get; set; }
        public List<sp_um_select_user_Result> UserList { get; set; }
        public CreateInfoModel Createinfo { get; set; }
    }
}