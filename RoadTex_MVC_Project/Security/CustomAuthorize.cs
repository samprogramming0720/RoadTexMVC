using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoadTex_MVC_Project.Models.Connection_Model;

namespace System.Web.Mvc
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private readonly int[] allowedroles;

        public CustomAuthorize(params int[] roles)
        {
            allowedroles = roles;
        }
 
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var IsAuthorized = base.AuthorizeCore(httpContext);
            if (!IsAuthorized)
                return false;
            foreach (var role in allowedroles)
            {
                using (Connection_Model.Connect())
                {
                    var user = Connection_Model.DB.tblUsers.Where(e => e.user_role_id == role 
                    && e.e_mail == HttpContext.Current.User.Identity.Name);
                    if (user.Count() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}