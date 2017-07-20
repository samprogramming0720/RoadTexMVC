using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadTex_MVC_Project.Models;
using RoadTex_MVC_Project.Models.RolePermission_Model;
using System.Data.Entity.Migrations;
using RoadTex_MVC_Project.Models.Connection_Model;

namespace RoadTex_MVC_Project.Controllers
{
    public class RolePermissionController : Controller
    {

        [NonAction]
        private void PopulateModel(RolePermissionModel Model)
        {
            using (Connection_Model.Connect())
            {
                Model.ListOfRoles = Connection_Model.DB.tblRoles.ToList();
                Model.ListOfPermission = Connection_Model.DB.tblPermissions.ToList();
                Model.RolePermStatus = Connection_Model.DB.tblRole_Permission.ToList();
            }
        }

        [NonAction]
        private void UpdateCheckBox(RolePermissionModel Model)
        {
            using (Connection_Model.Connect())
            {
                for (int i = 0; i < Model.ListOfRoles.Count; i++)
                {
                    for (int j = 0; j < Model.ListOfPermission.Count; j++)
                    {
                        tblRole_Permission RolePermission = new tblRole_Permission
                        {
                            role_id = Model.ListOfRoles[i].role_id,
                            perm_id = Model.ListOfPermission[j].perm_id,
                            edit_perm = Model.RowEditCheckBox[i].ColEditCheckBox[j].IsEditChecked,
                            view_perm = Model.RowViewCheckBox[i].ColViewCheckBox[j].IsViewChecked
                        };
                        Connection_Model.DB.tblRole_Permission.AddOrUpdate(e => new { e.role_id, e.perm_id }, RolePermission);
                    }
                }
                Connection_Model.DB.SaveChanges();
            }
        }
        [NonAction]
        private void GenerateCheckBox(RolePermissionModel Model)
        {
            using (Connection_Model.Connect())
            {
                for (int i = 0; i < Model.ListOfRoles.Count; i++)
                {
                    for (int j = 0; j < Model.ListOfPermission.Count; j++)
                    {
                        var temp_role = Model.ListOfRoles[i].role_id;
                        var temp_perm = Model.ListOfPermission[j].perm_id;
                        tblRole_Permission RolePermission = new tblRole_Permission
                        {
                            role_id = temp_role,
                            perm_id = temp_perm,
                            edit_perm = false,
                            view_perm = false
                        };
                        if (!Connection_Model.DB.tblRole_Permission.Where(e => e.role_id == temp_role
                        && e.perm_id == temp_perm).Any())
                        {
                            Connection_Model.DB.tblRole_Permission.Add(RolePermission);
                        }
                    }
                }
                Connection_Model.DB.SaveChanges();
                Model.RolePermStatus = Connection_Model.DB.tblRole_Permission.ToList();
            }
        }


        [NonAction]
        private void ShowCheckBox(RolePermissionModel Model)
        {
            Model.RowEditCheckBox = new List<ColumnEditCheckBoxList>();
            Model.RowViewCheckBox = new List<ColumnViewCheckBoxList>();
            int k = 0;
            for (int i = 0; i < Model.ListOfRoles.Count; i++)
            {
                ColumnEditCheckBoxList ColEditList = new ColumnEditCheckBoxList()
                {
                    ColEditCheckBox = new List<EditCheckBoxElement>()
                };
                ColumnViewCheckBoxList ColViewList = new ColumnViewCheckBoxList()
                {
                    ColViewCheckBox = new List<ViewCheckBoxElement>()
                };
                for (int j = 0; j < Model.ListOfPermission.Count; j++)
                {
                    EditCheckBoxElement EditEl = new EditCheckBoxElement();
                    ViewCheckBoxElement ViewEl = new ViewCheckBoxElement();
                    EditEl.IsEditChecked = Model.RolePermStatus[k].edit_perm;
                    ViewEl.IsViewChecked = Model.RolePermStatus[k].view_perm;
                    k++;
                    ColEditList.ColEditCheckBox.Add(EditEl);
                    ColViewList.ColViewCheckBox.Add(ViewEl);
                }
                Model.RowEditCheckBox.Add(ColEditList);
                Model.RowViewCheckBox.Add(ColViewList);
            }
        }

        [HttpGet]
        [CustomAuthorize(1)]
        public ActionResult RolePermission()
        {
            RolePermissionModel Model = new RolePermissionModel();
            PopulateModel(Model);
            GenerateCheckBox(Model);
            ShowCheckBox(Model);
            return View("RolePermission", Model);
        }

        [HttpPost]
        public ActionResult RolePermission(RolePermissionModel Model)
        {
            UpdateCheckBox(Model);
            return RedirectToAction("RolePermission");
        }
    }
}