using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadTex_MVC_Project.Models;

namespace RoadTex_MVC_Project.Controllers
{

  
    public class MasterDataController : Controller
    {
        RoadTex_MVC_Model_Server DB = new RoadTex_MVC_Model_Server();

        // GET: MasterData
        // [AuthorizeUserAccessLevel(UserRole = "Officer, Entry, IT Admin")]
        [CustomAuthorize(1,2,5)]
        public ActionResult MasterData()
        {
                vmMasterData master1 = new vmMasterData();
                master1.RegionName = new List<SelectListItem>();
                foreach (var r in DB.tblRegions.ToList())
                {
                    master1.RegionName.Add(new SelectListItem() { Text = r.region_name, Value = r.region_id.ToString() });

                }
                master1.States = new List<tblState>();
                var states = DB.sp_SelectStates();
                foreach (var state in states)
                {
                    tblState obj1 = new tblState();
                    obj1.state_name = state.state_name;
                    obj1.state_id = state.state_id;
                    obj1.region_id = state.region_id;
                    master1.States.Add(obj1);
                }
                return View(master1);

        }



        //[AuthorizeUserAccessLevel(UserRole = "Entry, IT Admin")]

        [CustomAuthorize(1, 2)]
        [HttpPost]
        public ActionResult MasterData(vmMasterData vm)
        {
    
            string state_name = vm.StateName;
            string r_id = Request.Form["Regions"].ToString();
            int region_id = Int32.Parse(r_id);

            var addState = DB.sp_AddState(state_name, region_id);
            if(addState == 1)
            {
                ViewBag.message = state_name + " is added to database";
            }
            else
            {
                ViewBag.message = "Failed to add state !";
            }

            
            return RedirectToAction("MasterData");
        }


    }
}