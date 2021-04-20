using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using pidevShoppyTounsi.Controllers;

namespace pidevShoppyTounsi.Models.Login
{
    public class RedirectingActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (LoginController.TokenConnect=="")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "LoginRequest"
                }));
            }
        }
    }
}