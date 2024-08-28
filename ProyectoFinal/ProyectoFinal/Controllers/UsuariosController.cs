using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class UsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Usuarios
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
            return View(db.Usuarios.ToList());

        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuario,codigoUsuario,nombreUsuario,password,ultimaFechaConexion,estadoUsuario")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuarios model, HttpPostedFileBase FotoPerfil)
        {
            if (ModelState.IsValid)
            {
                // Manejar la carga del archivo
                if (FotoPerfil != null && FotoPerfil.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(FotoPerfil.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Assets/FotosDePerfil"), fileName);

                    try
                    {
                        FotoPerfil.SaveAs(filePath);
                        model.FotoPerfilUrl = Url.Content("~/Assets/FotosDePerfil/" + fileName);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error al guardar la foto de perfil: " + ex.Message);
                    }
                }

                // Encontrar el usuario existente y actualizarlo
                var usuarioExistente = db.Usuarios.Find(model.id_usuario);
                if (usuarioExistente != null)
                {
                    usuarioExistente.codigoUsuario = model.codigoUsuario;
                    usuarioExistente.nombreUsuario = model.nombreUsuario;
                    usuarioExistente.password = model.password;
                    usuarioExistente.DireccionPrincipal = model.DireccionPrincipal;
                    usuarioExistente.MetodoDePago = model.MetodoDePago;
                    usuarioExistente.estadoUsuario = model.estadoUsuario;
                    usuarioExistente.FotoPerfilUrl = model.FotoPerfilUrl;

                    db.Entry(usuarioExistente).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Profile");
            }

            return View(model);
        }






        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ToggleStatus(int id)
        {
            // Obtén el usuario desde la base de datos
            var usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            // Cambia el estado del usuario
            usuario.estadoUsuario = !usuario.estadoUsuario;

            // Guarda los cambios en la base de datos
            db.SaveChanges();

            // Redirige de vuelta a la vista de listado
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

        // GET: Usuarios/Profile
        public ActionResult Profile()
        {
            
            var userName = User.Identity.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = db.Usuarios
                .Include(u => u.RegistroCompra.Select(rc => rc.Detalles.Select(d => d.Producto)))
                .SingleOrDefault(u => u.codigoUsuario == userName);

            if (usuario != null)
            {
                ViewBag.UserName = usuario.nombreUsuario;
                ViewBag.RolID = usuario.ID_Rol;
            }
            else
            {
                return HttpNotFound();
            }

            return View(usuario);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile([Bind(Include = "id_usuario,nombreUsuario,FotoPerfilUrl,Biografia")] Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View("Profile", usuario);
        }

        public ActionResult Test()
        {
            return Content("La acción Test funciona.");
        }
    }
}
