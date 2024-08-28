using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Usuarios model)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(model.FotoPerfilUrl))
                {
                    model.FotoPerfilUrl = "/Assets/img/ProfilePicture.jpg";
                }

                model.password = Crypto.HashPassword(model.password);

                model.ID_Rol = ObtenerRolClienteId();

                db.Usuarios.Add(model);
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(model.codigoUsuario, false);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private int ObtenerRolClienteId()
        {
            // Obtener el ID del rol "Cliente" desde la base de datos
            return db.Roles.FirstOrDefault(r => r.nombreRol == "Cliente")?.id_rol ?? 0;
        }

        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Usuarios.SingleOrDefault(u => u.codigoUsuario == model.codigoUsuario);
                if (user != null && Crypto.VerifyHashedPassword(user.password, model.password))
                {
                    FormsAuthentication.SetAuthCookie(user.codigoUsuario, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}