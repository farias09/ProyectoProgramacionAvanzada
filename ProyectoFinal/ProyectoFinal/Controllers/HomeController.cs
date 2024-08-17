using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var libros = db.Productos
                .Where(p => p.Categoria != null && p.Categoria.Nombre == "Libros")
                .OrderByDescending(p => p.id_producto)
                .Take(10)
                .ToList();

            var productosRecientes = db.Productos
                .Where(p => p.Categoria != null && p.Categoria.Nombre != "Libros")
                .OrderByDescending(p => p.id_producto)
                .Take(10)
                .ToList();

            ViewBag.ProductosRecientes = productosRecientes;

            return View(libros);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
