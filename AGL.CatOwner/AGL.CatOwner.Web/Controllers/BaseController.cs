using AGL.CatOwner.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AGL.CatOwner.Web.Controllers
{
    public class BaseController : Controller
    {
        internal Dictionary<string, string> routeValues; // = new Dictionary<string, string>();

        public BaseController() {
            //this.routeValues = new Dictionary<string, string>();
            //System.Web.Routing.RouteData routeData = System.Web.HttpContext.Current.Request.RequestContext.RouteData;
            //// this.routeValues.Add(Constants.area, routeData.DataTokens["area"].ToString());
            //this.routeValues.Add(Constants.controller, routeData.Values["controller"].ToString());
            //this.routeValues.Add(Constants.action, routeData.Values["action"].ToString());
            ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
        }
    }
}