using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyLoginRegistration.Security
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _role;
        public RoleAuthorizeAttribute(string role)
        {
            _role = role;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
             if (!httpContext.User.Identity.IsAuthenticated)
             {
                 return false;
             }

             return httpContext.User.IsInRole(_role);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                { 
                    { "controller", "Account" },
                    { "action", "Login" }
                });

                return;
            }

            filterContext.Controller.TempData["Error"] = "You are not authorized to access this page.";

            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
            {
                { "controller", "Account" },
                { "action", "LoggedIn" }
            });
        }


    }
}