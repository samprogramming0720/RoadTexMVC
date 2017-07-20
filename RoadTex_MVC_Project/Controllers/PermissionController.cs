using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadTex_MVC_Project.Models;
using RoadTex_MVC_Project.Models.Permission_Model;
using RoadTex_MVC_Project.Models.Connection_Model;

namespace RoadTex_MVC_Project.Controllers
{
    public class PermissionController : Controller
    {

        [NonAction]
        private void ShowPermissionList(PermissionModel Model)
        {
            using (Connection_Model.Connect())
            {
                Model.ListOfPermissions = Connection_Model.DB.tblPermissions.ToList();
            }
        }

        [NonAction]
        private void AddPermission(string permdesc)
        {
            using (Connection_Model.Connect())
            {
                Connection_Model.DB.tblPermissions.Add(new tblPermission
                {
                    permission = permdesc
                });
                Connection_Model.DB.SaveChanges();
            }
        }

        [CustomAuthorize(1,2)]
        public ActionResult Permission()
        {
            PermissionModel Model = new PermissionModel();
            ShowPermissionList(Model);
            return View("Permission", Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public JsonResult Create(PermissionModel Model)
        {
            string error = string.Empty;
            if (ModelState.IsValid)
            {
                AddPermission(Model.PermissionDescription);
                return Json(new { success = true, url = Url.Action("Permission", "Permission") });
            }
            else
            {
                List<System.Web.Mvc.ModelError> modelerrors = ModelState.SelectMany(x => x.Value.Errors).ToList();
                foreach (var modelerror in modelerrors)
                {
                    error += modelerror.ErrorMessage + "\n";
                }
                return Json(new { success = false, result = error });
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (Connection_Model.Connect())
                {
                    tblPermission permission = Connection_Model.DB.tblPermissions.Find(id);
                    Connection_Model.DB.Entry(permission).State = System.Data.Entity.EntityState.Deleted;
                    Connection_Model.DB.SaveChanges();
                }
            }
            catch
            {
                return Json(new { success = false, url = Url.Action("Permission", "Permission") });
            }
            return Json(new { success = true, url = Url.Action("Permission", "Permission") });
        }

        //Updates the user
        [HttpPost]
        public ActionResult Edit(int permId, string perminfo)
        {
            PermissionModel Model = new PermissionModel()
            {
                PermissionDescription = perminfo
            };
            using (Connection_Model.Connect())
            {
                tblPermission permission = Connection_Model.DB.tblPermissions.Find(permId);
                permission.permission = perminfo;
                Connection_Model.DB.Entry(permission).State = System.Data.Entity.EntityState.Modified;
                Connection_Model.DB.SaveChanges();
            }
            return Json(Url.Action("Permission", "Permission"));
        }

    }
}