using System.Web.Mvc;
using System.Web.Routing;

namespace HUTOPS.Helper
{
    public static class UrlHelperExtensions
    {
        public static string IsLinkActive(this UrlHelper url, string actionName, string controllerName)
        {
            return url.RequestContext.RouteData.Values["controller"].ToString() == controllerName && url.RequestContext.RouteData.Values["action"].ToString() == actionName ? "active" : "";
        }

        public static string IsTreeviewActive(this UrlHelper url, string controllerName)
        {
            string controller = url.RequestContext.RouteData.Values["controller"].ToString();
            return controllerName == controller ? "active" : "";
        }
    }
    public class SessionValidatorActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (filterContext.HttpContext.Session["UserId"] == null
                    //|| filterContext.HttpContext.Session["Email"] == null
                    )
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" } });
                }
            }
        }
    }
}