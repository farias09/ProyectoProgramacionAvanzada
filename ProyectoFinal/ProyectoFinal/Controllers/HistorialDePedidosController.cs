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
    public class HistorialDePedidosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HistorialDePedidos
        public ActionResult Index()
        {
            var historialDePedidos = db.HistorialDePedidos.Include(h => h.CarritoCompra).Include(h => h.Usuario);
            return View(historialDePedidos.ToList());
        }

        // GET: HistorialDePedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialDePedidos historialDePedidos = db.HistorialDePedidos.Find(id);
            if (historialDePedidos == null)
            {
                return HttpNotFound();
            }
            return View(historialDePedidos);
        }

        // GET: HistorialDePedidos/Create
        public ActionResult Create()
        {
            ViewBag.id_carritoCompra = new SelectList(db.CarritoDeCompras, "id_carritoCompra", "id_carritoCompra");
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario");
            return View();
        }

        // POST: HistorialDePedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_historialPedido,id_usuario,id_carritoCompra")] HistorialDePedidos historialDePedidos)
        {
            if (ModelState.IsValid)
            {
                db.HistorialDePedidos.Add(historialDePedidos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_carritoCompra = new SelectList(db.CarritoDeCompras, "id_carritoCompra", "id_carritoCompra", historialDePedidos.id_carritoCompra);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario", historialDePedidos.id_usuario);
            return View(historialDePedidos);
        }

        // GET: HistorialDePedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialDePedidos historialDePedidos = db.HistorialDePedidos.Find(id);
            if (historialDePedidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_carritoCompra = new SelectList(db.CarritoDeCompras, "id_carritoCompra", "id_carritoCompra", historialDePedidos.id_carritoCompra);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario", historialDePedidos.id_usuario);
            return View(historialDePedidos);
        }

        // POST: HistorialDePedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_historialPedido,id_usuario,id_carritoCompra")] HistorialDePedidos historialDePedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historialDePedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_carritoCompra = new SelectList(db.CarritoDeCompras, "id_carritoCompra", "id_carritoCompra", historialDePedidos.id_carritoCompra);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "codigoUsuario", historialDePedidos.id_usuario);
            return View(historialDePedidos);
        }

        // GET: HistorialDePedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialDePedidos historialDePedidos = db.HistorialDePedidos.Find(id);
            if (historialDePedidos == null)
            {
                return HttpNotFound();
            }
            return View(historialDePedidos);
        }

        // POST: HistorialDePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistorialDePedidos historialDePedidos = db.HistorialDePedidos.Find(id);
            db.HistorialDePedidos.Remove(historialDePedidos);
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
