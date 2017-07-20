using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadTex_MVC_Project.Models;
using RoadTex_MVC_Project.Models.UserManagement_Model;
using RoadTex_MVC_Project.Models.Connection_Model;

namespace RoadTex_MVC_Project.Controllers
{
    public class UserManagementController : Controller
    {


        //Populate List
        [NonAction]
        private void PopulateRolesList(UMViewModel Model)
        {
            using (Connection_Model.Connect())
            {
                Model.RolesList = new SelectList(Connection_Model.DB.tblRoles.ToList(), "role_id", "role_name");
            }
        }

        //Show all users
        [NonAction]
        private void ShowAllUsers(UMViewModel Model)
        {
            using (Connection_Model.Connect())
            {
                Model.UserList = Connection_Model.DB.sp_um_select_user().ToList();
            }
        }

        //Create a user
        [NonAction]
        private int CreateUser(UMViewModel Model)
        {
            using (Connection_Model.Connect())
            {
                return Connection_Model.DB.sp_um_create_user
                    (
                    Model.Createinfo.FirstName,
                    Model.Createinfo.LastName,
                    Model.Createinfo.Email,
                    Model.Createinfo.Password,
                    Model.Createinfo.Role,
                    Model.Createinfo.IsSalesRep,
                    Model.Createinfo.IsPreparer
                    );
            }
        }

        //Updates a user
        [NonAction]
        private int UpdateUser(UMViewModel Model, int id)
        {
            using (Connection_Model.Connect())
            {
                return Connection_Model.DB.sp_um_update_user
                    (
                    Model.Userinfo.FirstName,
                    Model.Userinfo.LastName,
                    Model.Userinfo.Email,
                    Model.Userinfo.Role,
                    Model.Userinfo.IsSalesRep,
                    Model.Userinfo.IsPreparer,
                    id
                    );
            }
        }

        //Deletes a user that matches the id field
        [NonAction]
        private int DeleteUser(int id)
        {
            using (Connection_Model.Connect())
            {
                return Connection_Model.DB.sp_um_delete_user(id);
            }
        }

        //Fetches one user that matches the id field
        [NonAction]
        private tblUser GetUser(int id)
        {
            using (Connection_Model.Connect())
            {
                return Connection_Model.DB.tblUsers.Where(e => e.user_id == id).FirstOrDefault();
            }
        }

        //First User Management
        //Populates Roles List
        //Shows all user list
        [CustomAuthorize(1)]
        [HttpGet]
        public ActionResult UserManagement()
        {
            UMViewModel Model = new UMViewModel();
            PopulateRolesList(Model);
            ShowAllUsers(Model);
            return View("UserManagement", Model);
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            UMViewModel Model = new UMViewModel();
            PopulateRolesList(Model);
            return PartialView("Create", Model);
        }

        //Creates User
        [HttpPost]
        public JsonResult Create(CreateInfoModel userinfo)
        {
            string error = string.Empty;
            UMViewModel Model = new UMViewModel()
            {
                Createinfo = userinfo
            };
            if (ModelState.IsValid)
            {
                CreateUser(Model);
                return Json(new { success = true, url = Url.Action("UserManagement", "UserManagement") });
            }
            else
            {
                List<System.Web.Mvc.ModelError> modelerrors = ModelState.SelectMany(x => x.Value.Errors).ToList();
                foreach (var modelerror in modelerrors)
                {
                    error += modelerror.ErrorMessage + "\n";
                }
                PopulateRolesList(Model);
                return Json(new { success = false, result = error });
            }
        }

        //Delete a user
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                DeleteUser(id);
            }
            catch
            {
                return Json(new { success = false, url = Url.Action("UserManagement", "UserManagement") });
            }          
            return Json( new { success = true, url = Url.Action("UserManagement", "UserManagement") });
        }

        //Updates the user
        [HttpPost]
        public ActionResult Edit(int userId, UserInfoModel userinfo)
        {
            UMViewModel Model = new UMViewModel()
            {
                Userinfo = userinfo
            };
            UpdateUser(Model, userId);
            return Json(Url.Action("UserManagement", "UserManagement"));
        }
    }
}