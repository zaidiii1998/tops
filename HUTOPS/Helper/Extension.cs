using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static System.Net.Mime.MediaTypeNames;

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
                if (HttpContext.Current.Session[Constants.Session.UserId] == null
                    //|| filterContext.HttpContext.Session["Email"] == null
                    )
                {
                    
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" } });
                }
            }
        }
    }
    public static class Extensions
    {
        public static List<SelectListItem> ConvertToSelect<T>(List<T> sourceList, Func<T, int> valueSelector, Func<T, string> textSelector)
        {
            var select = new List<SelectListItem>();
           
            foreach (var item in sourceList)
            {
                select.Add(new SelectListItem
                {
                    Value = valueSelector(item).ToString(),
                    Text = textSelector(item).ToString()
                });
            }
            
            return select.ToList();
        }
    }
}