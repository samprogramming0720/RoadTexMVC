using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RoadTex_MVC_Project.Models.Login_Model
{
    public class Credential
    {
        [Required]
        [Display(Name = "E-mail")]
        public string E_mail { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string User_password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }

    public class forgotPasswordValidation
    {
        [Required(ErrorMessage = "Must enter Email Address")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Must enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter same password to confirm")]
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Password did not matched")]
        public string ConfirmPassword { get; set; }

    }
}