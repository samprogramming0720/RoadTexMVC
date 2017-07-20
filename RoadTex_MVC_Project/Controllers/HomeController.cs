using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RoadTex_MVC_Project.Models;
using RoadTex_MVC_Project.Models.Login_Model;
using RoadTex_MVC_Project.Models.Connection_Model;

namespace RoadTex_MVC_Project.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        public ActionResult Regions(string region_name, string region_description)
        {
            using (Connection_Model.Connect())
            {

                string regionName = region_name;
                string regionDescription = region_description;

                int result = Connection_Model.DB.sp_AddRegion(regionName, regionDescription);
                if (result == 1)
                {
                    @ViewBag.message = "Added Region Successfully";
                }
                else
                {
                    ViewBag.message = "Error While Adding Region";
                }

            }
            return View("Regions");
        }

        [HttpGet]
        public ActionResult Regions()
        {
            return View("Regions");
        }

        public ActionResult MasterData()
        {
            return View("MasterData");
        }

        public ActionResult Login()
        {
            Credential Credentials = new Credential();
            if (Request.Cookies["Login"] != null)
            {
                Credentials.E_mail = Request.Cookies["Login"].Values["Email"];
                Credentials.RememberMe = true;
            }
            else
            {
                Credentials.E_mail = null;
                Credentials.RememberMe = false;
            }
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Index", "CustomerDashBoard");
            }
            return View("Login", Credentials);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Credential Credentials)
        {
            if (ModelState.IsValid)
            {
                using (Connection_Model.Connect())
                {
                    var User = Connection_Model.DB.tblUsers.Where(e => e.e_mail.Equals(Credentials.E_mail) && 
                    e.user_password.Equals(Credentials.User_password)).FirstOrDefault();
                    if (User != null)
                    {
                      
                        FormsAuthentication.SetAuthCookie(Credentials.E_mail, false);
                        HttpCookie cookie = new HttpCookie("Login");
                        if (Credentials.RememberMe)
                        {
                            cookie.Values.Add("Email", Credentials.E_mail);
                            Response.Cookies.Add(cookie);
                        }
                        else
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(cookie);
                        }
                        return RedirectToAction("Index", "CustomerDashBoard");
                    }
                    else
                    {
                        ModelState.AddModelError("NoRecord", "Invalid username or password");
                    }
                }
            }
            return View("Login", Credentials);
        }

        public ActionResult DashBoard()
        {
            if (Session["UserName"] != null)
            {
                return View("DashBoard");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult forgotPassword()
        {
            return View("forgotPassword");
        }

        [HttpPost]
        public ActionResult forgotPassword(forgotPasswordValidation fpval)
        {

            string email = fpval.Email.ToString();
            string confirmPass = fpval.ConfirmPassword.ToString();
            using (Connection_Model.Connect())
            {

                int result = Connection_Model.DB.sp_ResetPassword(email, confirmPass);
                if (result == 1)
                {
                    @ViewBag.message = "success";


                    return View("forgotPassword");
                }
                else
                {
                    @ViewBag.message = "fail!";

                    return View("forgotPassword");
                }


            }

        }

    }
}