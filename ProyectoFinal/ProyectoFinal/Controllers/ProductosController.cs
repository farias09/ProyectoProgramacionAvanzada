using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Catalogo(List<int> categorias, string ordenarPor)
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

            IQueryable<Productos> productos = db.Productos.Include(p => p.Categoria);

            if (categorias != null && categorias.Any())
            {
                productos = productos.Where(p => categorias.Contains(p.CategoriaId));
            }

            // Ordenar los productos según el valor de ordenarPor
            switch (ordenarPor)
            {
                case "PrecioAsc":
                    productos = productos.OrderBy(p => p.precioProducto);
                    break;
                case "PrecioDesc":
                    productos = productos.OrderByDescending(p => p.precioProducto);
                    break;
                default:
                    break;
            }

            // Inicializa ViewBag.Categorias si es null
            var categoriasList = db.Categorias.ToList();
            ViewBag.Categorias = categoriasList ?? new List<Categorias>();
            ViewBag.CategoriasSeleccionadas = categorias ?? new List<int>();
            ViewBag.OrdenarPor = ordenarPor ?? string.Empty;

            return View(productos.ToList());
        }



        // GET: Productos
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
            var productos = db.Productos.Include(p => p.Categoria);
            return View(productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Include(p => p.Categoria).FirstOrDefault(p => p.id_producto == id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }


        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre");
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_producto,codigoProducto,nombreProducto,descripcion,precioProducto,disponibilidadInventario,imagenAlmacenada,estadoProducto,CategoriaId")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", productos.CategoriaId);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", productos.CategoriaId);
            return View(productos);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_producto,codigoProducto,nombreProducto,descripcion,precioProducto,disponibilidadInventario,imagenAlmacenada,estadoProducto,CategoriaId")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                var productoExistente = db.Productos.Find(productos.id_producto);
                if (productoExistente != null)
                {
                    db.Entry(productoExistente).CurrentValues.SetValues(productos);
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                ModelState.AddModelError(string.Empty, validationError.ErrorMessage);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error al guardar los cambios. " + ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No se encontró el producto.");
                }
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", productos.CategoriaId);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Include(p => p.Categoria).FirstOrDefault(p => p.id_producto == id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            try
            {
                db.Productos.Remove(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el producto. " + ex.Message);
                return View(productos);
            }
        }


        // Método para mostrar la información del producto
        public ActionResult InfoProducto(int id)
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

            var producto = db.Productos
                .Include(p => p.Reseña)
                .Include(p => p.Reseña.Select(r => r.Usuario))
                .FirstOrDefault(p => p.id_producto == id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        // Método para agregar una nueva reseña
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(Reseñas reseña)
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
            if (ModelState.IsValid)
            {
                reseña.fechaPublicacion = DateTime.Now;
                db.Resenas.Add(reseña);
                db.SaveChanges();
                return RedirectToAction("InfoProducto", new { id = reseña.id_producto });
            }
            var producto = db.Productos.Include(p => p.Reseña.Select(r => r.Usuario)).FirstOrDefault(p => p.id_producto == reseña.id_producto);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = User.Identity.GetUserId();
            return View("InfoProducto", producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}