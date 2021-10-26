using Joselito_Technocell.Helpers;
using Joselito_Technocell.Models;
using Joselito_Technocell.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Joselito_Technocell.Controllers
{
    [Authorize(Roles = "AD-ROOT")]
    public class UsersController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();
        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<User>(new UserStore<User>(db));
            var usuarios = userManager.Users.ToList();
            return View(usuarios);
        }

        public ActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                UserHelper.CreateUserASP(user, "Entrada123.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(user);
            }
        }

        public ActionResult Roles(string usuarioID)
        {
            if (string.IsNullOrEmpty(usuarioID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();

            var usuarios = userManager.Users.ToList();
            var usuario = usuarios.Find(u => u.Id == usuarioID);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var rolesView = new List<RolView>();


            foreach (var item in usuario.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RolView
                {

                    RolID = role.Id,
                    Nombre = role.Name
                };
                rolesView.Add(roleView);
            }


            var vistaUsuario = new VistaUsuario
            {
                Email = usuario.Email,
                Nombre = usuario.FullName,
                UsuarioID = usuario.Id,
                Roles = rolesView
            };

            return View(vistaUsuario);

        }


        public ActionResult AgregarRoles(string usuarioID)
        {
            if (string.IsNullOrEmpty(usuarioID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<User>(new UserStore<User>(db));
            var usuarios = userManager.Users.ToList();
            var usuario = usuarios.Find(u => u.Id == usuarioID);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var vistaUsuario = new VistaUsuario
            {
                Email = usuario.Email,
                Nombre = usuario.UserName,
                UsuarioID = usuario.Id
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var list = roleManager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "-- Selecione un Rol --" });
            list = list.OrderBy(o => o.Name).ToList();
            ViewBag.RolID = new SelectList(list, "Id", "Name");

            return View(vistaUsuario);
        }


        [HttpPost]
        public ActionResult AgregarRoles(string usuarioID, FormCollection form)
        {
            var rolID = Request["RolID"];

            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usuarios = userManager.Users.ToList();
            var usuario = usuarios.Find(u => u.Id == usuarioID);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var vistaUsuario = new VistaUsuario
            {
                Email = usuario.Email,
                Nombre = usuario.UserName,
                UsuarioID = usuario.Id
            };

            if (string.IsNullOrEmpty(rolID))
            {
                ViewBag.Error = "Debe seleccionar un rol";

                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "-- Selecione un Rol --" });
                list = list.OrderBy(o => o.Name).ToList();
                ViewBag.RolID = new SelectList(list, "Id", "Name");

                return View(vistaUsuario);
            }

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(b => b.Id == rolID);

            if (!userManager.IsInRole(usuarioID, role.Name))
            {
                userManager.AddToRole(usuarioID, role.Name);
            }

            var rolesView = new List<RolView>();


            foreach (var item in usuario.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RolView
                {

                    RolID = role.Id,
                    Nombre = role.Name
                };
                rolesView.Add(roleView);
            }


            vistaUsuario = new VistaUsuario
            {
                Email = usuario.Email,
                Nombre = usuario.UserName,
                UsuarioID = usuario.Id,
                Roles = rolesView
            };

            return View("Roles", vistaUsuario);
        }

        public ActionResult Delete(string usuarioID, string rolID)
        {
            if (string.IsNullOrEmpty(usuarioID) || string.IsNullOrEmpty(rolID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<User>(new UserStore<User>(db));

            var usuario = userManager.Users.ToList().Find(u => u.Id == usuarioID);
            var rol = roleManager.Roles.ToList().Find(r => r.Id == rolID);

            if (userManager.IsInRole(usuario.Id, rol.Name))
            {
                userManager.RemoveFromRole(usuario.Id, rol.Name);
            }

            var usuarios = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RolView>();

            foreach (var item in usuario.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RolView
                {
                    RolID = role.Id,
                    Nombre = role.Name
                };
                rolesView.Add(roleView);
            }

            var vistaUsuario = new VistaUsuario
            {
                Email = usuario.Email,
                Nombre = usuario.UserName,
                UsuarioID = usuario.Id,
                Roles = rolesView
            };

            return View("Roles", vistaUsuario);

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