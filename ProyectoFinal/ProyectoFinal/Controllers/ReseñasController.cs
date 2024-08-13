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
    public class ReseñasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reseñas
        public ActionResult Index()
        {
            var resenas = db.Resenas.Include(r => r.Producto).Include(r => r.Usuario);
            return View(resenas.ToList());
        }

        // GET: Reseñas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseñas reseñas = db.Resenas.Find(id);
            if (reseñas == null)
            {
                return HttpNotFound();
            }
            return View(reseñas);
        }

        // GET: Reseñas/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "codigoProducto");
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario");
            return View();
        }

        // POST: Reseñas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // POST: Reseñas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id_producto, string titulo, string descripcion)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.codigoUsuario == User.Identity.Name);
                if (usuario != null)
                {
                    var reseña = new Reseñas
                    {
                        id_producto = id_producto,
                        id_usuario = usuario.id_usuario,
                        titulo = titulo,
                        descripcion = descripcion,
                        fechaPublicacion = DateTime.Now
                    };

                    db.Resenas.Add(reseña);
                    db.SaveChanges();
                    return RedirectToAction("InfoProducto", "Productos", new { id = id_producto });
                }
            }

            return RedirectToAction("InfoProducto", "Productos", new { id = id_producto });
        }


        // GET: Reseñas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseñas reseñas = db.Resenas.Find(id);
            if (reseñas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "codigoProducto", reseñas.id_producto);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario", reseñas.id_usuario);
            return View(reseñas);
        }

        // POST: Reseñas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_reseña,fechaPublicacion,descripcion,id_usuario,id_producto")] Reseñas reseñas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reseñas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "codigoProducto", reseñas.id_producto);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario", reseñas.id_usuario);
            return View(reseñas);
        }

        // GET: Reseñas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reseñas reseñas = db.Resenas.Find(id);
            if (reseñas == null)
            {
                return HttpNotFound();
            }
            return View(reseñas);
        }

        // POST: Reseñas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reseñas reseñas = db.Resenas.Find(id);
            db.Resenas.Remove(reseñas);
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
    }
}
