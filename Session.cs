using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["Uid"] == null)
            {
                filterContext.Result = new RedirectResult("~/Auth/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}