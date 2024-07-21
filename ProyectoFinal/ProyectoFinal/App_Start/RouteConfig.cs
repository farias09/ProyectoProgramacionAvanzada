using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapRoute(
            name: "Catalogo",
            url: "Catalogo",
            defaults: new { controller = "Productos", action = "Catalogo" }
        );

        routes.MapRoute(
            name: "InfoProducto",
            url: "Producto/{id}",
            defaults: new { controller = "Productos", action = "InfoProducto" }
        );

        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
    }
}
