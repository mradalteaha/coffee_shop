using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace coffee_shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        

            routes.MapRoute(
             name: "Login",
             url: "Home/Login",
             defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }


        );


        routes.MapRoute(
        name: "Admin",
        url: "UserModels/Admin",
        defaults: new { controller = "UserModels", action = "Admin", id = UrlParameter.Optional }
                );

            routes.MapRoute(
            name: "Barista",
            url: "UserModels/Barista",
            defaults: new { controller = "UserModels", action = "Barista", id = UrlParameter.Optional }
                    );

            routes.MapRoute(
        name: "Edit",
        url: "UserModels/Edit",
        defaults: new { controller = "UserModels", action = "Admin", id = UrlParameter.Optional }
                );

            routes.MapRoute(
        name: "AddTinside",
        url: "UserModels/AddTinside",
        defaults: new { controller = "UserModels", action = "Admin", id = UrlParameter.Optional }
                );



        }



    }
}
