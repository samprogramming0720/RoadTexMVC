using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadTex_MVC_Project.Models.RolePermission_Model
{
    public class RolePermissionModel
    {
        public List<tblRole> ListOfRoles { get; set; }
        public List<tblPermission> ListOfPermission { get; set; }
        public List<tblRole_Permission> RolePermStatus { get; set; }
        public List<ColumnEditCheckBoxList> RowEditCheckBox { get; set; }
        public List<ColumnViewCheckBoxList> RowViewCheckBox { get; set; }
    }

    public class ColumnEditCheckBoxList
    {
        public List<EditCheckBoxElement> ColEditCheckBox { get; set; }
    }

    public class ColumnViewCheckBoxList
    {
        public List<ViewCheckBoxElement> ColViewCheckBox { get; set; }
    }

    public class EditCheckBoxElement
    {
        public bool IsEditChecked { get; set; }
    }

    public class ViewCheckBoxElement
    {
        public bool IsViewChecked { get; set; }
    }
}