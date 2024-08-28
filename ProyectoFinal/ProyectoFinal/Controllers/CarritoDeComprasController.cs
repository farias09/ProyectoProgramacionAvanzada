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

        public ActionResult EliminarDelCarrito(int productoId)
        {
            // Obtener el carrito de la sesión
            CarritoDeCompras carrito = ObtenerCarritoDeLaSesion();

            // Eliminar el producto del carrito
            carrito.EliminarProducto(productoId);

            // Actualizar la sesión con el carrito actualizado
            Session[CarritoSessionKey] = carrito;

            // Redirigir de vuelta al índice del carrito
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ConfirmacionCompra()
        {
            var carrito = ObtenerCarritoDeLaSesion();
            if (carrito == null || carrito.Items.Count == 0)
            {
                // Maneja el caso donde el carrito está vacío
                return RedirectToAction("Index", "Home");
            }

            var registroCompra = ProcesarPedido(carrito);

            // Limpiar el carrito de la sesión después de la compra
            Session[CarritoSessionKey] = null;

            // Pasar el modelo a la vista
            return View("ConfirmacionCompra", registroCompra);
        }


        private RegistroCompra ProcesarPedido(CarritoDeCompras carrito)
        {
            // Obtener el nombre de usuario del usuario autenticado
            var userName = User.Identity.Name;

            if (string.IsNullOrEmpty(userName))
            {
                // Maneja el caso en que el usuario no está autenticado
                throw new InvalidOperationException("Usuario no autenticado.");
            }

            // Buscar el usuario en la base de datos usando el nombre de usuario
            var usuario = db.Usuarios.SingleOrDefault(u => u.codigoUsuario == userName);

            if (usuario == null)
            {
                // Maneja el caso en que el usuario no se encuentra
                throw new InvalidOperationException("Usuario no encontrado.");
            }

            var registroCompra = new RegistroCompra
            {
                FechaCompra = DateTime.Now,
                MontoTotal = Convert.ToDecimal(carrito.montoTotal),
                UsuarioId = usuario.id_usuario, // Asignar el ID del usuario autenticado
                Detalles = new List<DetalleCompra>()
            };

            foreach (var item in carrito.Items)
            {
                var producto = db.Productos.Find(item.Producto.id_producto);
                if (producto != null)
                {
                    var detalle = new DetalleCompra
                    {
                        ProductoId = producto.id_producto,
                        Cantidad = item.Cantidad,
                        RegistroCompra = registroCompra,
                        Producto = producto 
                    };

                    registroCompra.Detalles.Add(detalle);
                }
            }

            db.RegistroCompras.Add(registroCompra);
            db.SaveChanges();

            return registroCompra;
        }

    }
}
