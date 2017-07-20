﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RoadTex_MVC_Project.Models.UserManagement_Model
{
    public class UserInfoModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Role")]
        public int Role { get; set; }
        [Display(Name = "is Saels Rep")]
        public bool IsSalesRep { get; set; }
        [Display(Name = "is Preparer")]
        public bool IsPreparer { get; set; }
    }
}