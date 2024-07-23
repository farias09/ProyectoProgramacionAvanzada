using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class CarritoDeComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private const string CarritoSessionKey = "Carrito";

        // GET: CarritoDeCompras
        public ActionResult Index()
        {
            CarritoDeCompras carrito = ObtenerCarritoDeLaSesion();
            return View(carrito.Items);
        }

        // GET: CarritoDeCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarritoDeCompras carritoDeCompras = db.CarritoDeCompras.Find(id);
            if (carritoDeCompras == null)
            {
                return HttpNotFound();
            }
            return View(carritoDeCompras);
        }

        // GET: CarritoDeCompras/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "codigoProducto");
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario");
            return View();
        }

        // POST: CarritoDeCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_carritoCompra,cantidad,montoTotal,fechaCompra,id_usuario,id_producto")] CarritoDeCompras carritoDeCompras)
        {
            if (ModelState.IsValid)
            {
                db.CarritoDeCompras.Add(carritoDeCompras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "codigoProducto", carritoDeCompras.id_producto);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario", carritoDeCompras.id_usuario);
            return View(carritoDeCompras);
        }

        // GET: CarritoDeCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarritoDeCompras carritoDeCompras = db.CarritoDeCompras.Find(id);
            if (carritoDeCompras == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "codigoProducto", carritoDeCompras.id_producto);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario", carritoDeCompras.id_usuario);
            return View(carritoDeCompras);
        }

        // POST: CarritoDeCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_carritoCompra,cantidad,montoTotal,fechaCompra,id_usuario,id_producto")] CarritoDeCompras carritoDeCompras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carritoDeCompras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "codigoProducto", carritoDeCompras.id_producto);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario", carritoDeCompras.id_usuario);
            return View(carritoDeCompras);
        }

        // GET: CarritoDeCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarritoDeCompras carritoDeCompras = db.CarritoDeCompras.Find(id);
            if (carritoDeCompras == null)
            {
                return HttpNotFound();
            }
            return View(carritoDeCompras);
        }

        // POST: CarritoDeCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarritoDeCompras carritoDeCompras = db.CarritoDeCompras.Find(id);
            db.CarritoDeCompras.Remove(carritoDeCompras);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult VerificarAcceso()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index");
        }

        private CarritoDeCompras ObtenerCarritoDeLaSesion()
        {
            var carrito = Session[CarritoSessionKey] as CarritoDeCompras;
            if (carrito == null)
            {
                carrito = new CarritoDeCompras();
                Session[CarritoSessionKey] = carrito;
            }
            return carrito;
        }

        // Acción para agregar un producto al carrito
        public ActionResult AgregarAlCarrito(int productoId)
        {
            // Obtener el producto desde la base de datos 
            Productos producto = db.Productos.Find(productoId); 

            if (producto != null)
            {
                // Obtener el carrito de la sesión
                CarritoDeCompras carrito = ObtenerCarritoDeLaSesion();

                // Agregar el producto al carrito
                carrito.AgregarProducto(producto);

                // Actualizar la sesión con el carrito actualizado
                Session[CarritoSessionKey] = carrito;
            }

            // Redirigir a la vista del carrito o a la página de productos
            return RedirectToAction("Index");
        }
    }
}
