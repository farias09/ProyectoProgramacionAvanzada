using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class PagoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const string CarritoSessionKey = "Carrito";
        // GET: Pago/Checkout
        public ActionResult Checkout()
        {
            CarritoDeCompras carrito = (CarritoDeCompras)Session["Carrito"];
            if (carrito == null || carrito.Items.Count == 0)
            {
                return RedirectToAction("Index", "CarritoDeCompras");
            }

            Pago model = new Pago
            {
                Items = carrito.Items,
               //MontoTotal = Items.Sum(i => i.Cantidad * i.Producto.precioProducto);
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(Pago model)
        {
            // Aquí puedes manejar la lógica de pago según el método seleccionado
            // Ej. guardar información en la base de datos, procesar el pago, etc.

            // Redirigir a una página de confirmación o resumen
            return RedirectToAction("Confirmacion");
        }

        public ActionResult Confirmacion()
        {
            return View();
        }
    }
}