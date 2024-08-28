using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                // Verificar si el nombre de usuario se puede usar para buscar al usuario en la base de datos
                var usuario = db.Usuarios
                    .SingleOrDefault(u => u.codigoUsuario == userName); 

                if (usuario != null)
                {
                    ViewBag.UserName = usuario.nombreUsuario;
                    ViewBag.RolID = usuario.ID_Rol; 
                }
            }

            return View(libros);
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                // Verificar si el nombre de usuario se puede usar para buscar al usuario en la base de datos
                var usuario = db.Usuarios
                    .SingleOrDefault(u => u.codigoUsuario == userName);

                if (usuario != null)
                {
                    ViewBag.UserName = usuario.nombreUsuario;
                    ViewBag.RolID = usuario.ID_Rol;
                }
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                // Verificar si el nombre de usuario se puede usar para buscar al usuario en la base de datos
                var usuario = db.Usuarios
                    .SingleOrDefault(u => u.codigoUsuario == userName);

                if (usuario != null)
                {
                    ViewBag.UserName = usuario.nombreUsuario;
                    ViewBag.RolID = usuario.ID_Rol;
                }
            }

            return View();
        }
    }
}
