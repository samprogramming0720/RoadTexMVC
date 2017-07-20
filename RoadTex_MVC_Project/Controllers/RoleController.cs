using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadTex_MVC_Project.Models;
using RoadTex_MVC_Project.Models.Role_Model;
using RoadTex_MVC_Project.Models.Connection_Model;

namespace RoadTex_MVC_Project.Controllers
{
    public class RoleController : Controller
    {

        [NonAction]
        private void ShowRolesList(RoleModel Model)
        {
            using (Connection_Model.Connect())
            {
                Model.ListOfRoles = Connection_Model.DB.tblRoles.ToList();
            }
        }
        
        [NonAction]
        private void AddRole(string roledesc)
        {
            using (Connection_Model.Connect())
            {
                Connection_Model.DB.tblRoles.Add(new tblRole
                {
                    role_name = roledesc
                });
                Connection_Model.DB.SaveChanges();
            }
        }

        [CustomAuthorize(1)]
        public ActionResult Role()
        {
            RoleModel Model = new RoleModel();
            ShowRolesList(Model);
            return View("Role", Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public JsonResult Create(RoleModel Model)
        {
            string error = string.Empty;
            if (ModelState.IsValid)
            {
                AddRole(Model.RoleDescription);
                return Json(new { success = true, url = Url.Action("Role", "Role") });
            }
            else
            {
                List<System.Web.Mvc.ModelError> modelerrors = ModelState.SelectMany(x => x.Value.Errors).ToList();
                foreach (var modelerror in modelerrors)
                {
                    error += modelerror.ErrorMessage + "\n";
                }
                return Json(new { success = false, result=error });
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (Connection_Model.Connect())
                {
                    tblRole Role = Connection_Model.DB.tblRoles.Find(id);
                    Connection_Model.DB.Entry(Role).State = System.Data.Entity.EntityState.Deleted;
                    Connection_Model.DB.SaveChanges();
                }
            }
            catch
            {
                return Json(new { success = false, url = Url.Action("Role", "Role") });
            }
            return Json(new { success = true, url = Url.Action("Role", "Role") });
        }

        //Updates the user
        [HttpPost]
        public ActionResult Edit(int roleId, string roleinfo)
        {
            RoleModel Model = new RoleModel()
            {
                RoleDescription = roleinfo
            };
            using (Connection_Model.Connect())
            {
                tblRole Role = Connection_Model.DB.tblRoles.Find(roleId);
                Role.role_name = roleinfo;
                Connection_Model.DB.Entry(Role).State = System.Data.Entity.EntityState.Modified;
                Connection_Model.DB.SaveChanges();
            }
            return Json(Url.Action("Role", "Role"));
        }
    }
}