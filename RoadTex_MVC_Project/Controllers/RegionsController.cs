using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadTex_MVC_Project.Models;
using RoadTex_MVC_Project.Models.Regions;
using RoadTex_MVC_Project.Models.Connection_Model;

namespace RoadTex_MVC_Project.Controllers
{
    //[AuthorizeUserAccessLevel(UserRole = "IT Admin, Entry, Officer")]
    public class RegionsController : Controller
    {
        [CustomAuthorize(1,2, 5)]
        public ActionResult Regions()
        {
                return View();
        }
        [CustomAuthorize(1, 2)]
        // [AuthorizeUserAccessLevel(UserRole = "IT Admin, Entry")]    
        [HttpPost]
        public ActionResult Regions(Models.Regions.RegionsValidation rv)
        {
            using (Connection_Model.Connect())
            {

                string regionName = rv.RegionName;
                string regionDescription = rv.RegionDescription;

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
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }


    }
}




       
