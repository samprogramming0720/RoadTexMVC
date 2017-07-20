using RoadTex_MVC_Project.Models;
using System.Web.Mvc;

namespace System.Web.Mvc
{

    public class AuthorizeUserAccessLevel : AuthorizeAttribute 
    {
        
        public string UserRole { get; set; }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
    
            RoadTex_MVC_Model_Server DB = new RoadTex_MVC_Model_Server();
            int roleId =Convert.ToInt32(httpContext.Session["Role"]);
            var result = DB.sp_getRole(roleId);
            tblRole role = new tblRole();
            foreach (var item in result)
            {                
                role.role_id = item.role_id;
                role.role_name = item.role_name;
            }

            string CurrentUserRole = role.role_name;
            if (this.UserRole.Contains(CurrentUserRole))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}