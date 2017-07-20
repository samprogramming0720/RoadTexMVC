using RoadTex_MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoadTex_MVC_Project.Controllers
{
   // [AuthorizeUserAccessLevel(UserRole = "IT Admin")]
    [CustomAuthorize(1)]
    public class EditStateController : Controller
    {
        [HttpPost]
        public ActionResult EditState(int state_id,string state_name)
        {
            var stateId = Int32.Parse(Request["state_id"]);
            var name = Request["state_name"];
            RoadTex_MVC_Model_Server DB = new RoadTex_MVC_Model_Server();
            var updateState = DB.sp_UpdateState(stateId, name);
           // var updated = DB.sp_UpdatedRow(stateId);   
            //tblState obj = new tblState();
            //foreach (var u in updated)
            //{
            //    obj.state_id = u.state_id;        
            //    obj.state_name = u.state_name;
            //    obj.region_id = u.region_id;
            //}

            return View();
            //DB.Entry(obj).State = EntityState.Modified;
            //return Json(new { Url = Url.Action("PartialViewMaster") });
        }

        //public ActionResult PartialViewMaster()
        //{
        //    RoadTex_MVC_Model_Server DB = new RoadTex_MVC_Model_Server();
        //    vmMasterData master1 = new vmMasterData();
        //    master1.RegionName = new List<SelectListItem>();
        //    foreach (var r in DB.tblRegions.ToList())
        //    {
        //        master1.RegionName.Add(new SelectListItem() { Text = r.region_name, Value = r.region_id.ToString() });

        //    }
        //    master1.States = new List<tblState>();
        //    var states = DB.sp_SelectStates();
        //    foreach (var state in states)
        //    {
        //        tblState obj1 = new tblState();
        //        obj1.state_name = state.state_name;
        //        obj1.state_id = state.state_id;
        //        obj1.region_id = state.region_id;
        //        master1.States.Add(obj1);
        //    }
        //    return PartialView("_StatesDisplay",master1);
        //}
    }
}