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

        // GET: CarritoDeCompras
        public ActionResult Index()
        {
            var carritoDeCompras = db.CarritoDeCompras.Include(c => c.Producto).Include(c => c.Usuario);
            return View(carritoDeCompras.ToList());
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
    }
}
