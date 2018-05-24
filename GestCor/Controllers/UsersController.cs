using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Users-Leer")]
        public ActionResult Index()
        {
            UserGestCor user = new UserGestCor();

            return View(user.GetUsers());
        }

        // GET: Users/Create
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Users-Crear")]
        public ActionResult Create()
        {
            try
            {
                GestCorProfile profile = new GestCorProfile();

                List<SelectListItem> listRol = profile.getRolesAvaliable(profile.RolId);

                ViewData["roles"] = listRol;
                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Users/Create
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Users-Crear")]
        public ActionResult Create(UserGestCor model, int roles)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.IdRol = roles;
                if (model.SaveUser())
                {
                    TempData["AlertMessage"] = "USUARIO CREADO CON EXITO";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al crear el usuario";
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Users/Edit/5
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Users-Editar")]
        public ActionResult Edit(int id)
        {
            try
            {
                UserGestCor user = new UserGestCor();
                user = user.GetUsersById(id);

                GestCorProfile profileUser = new GestCorProfile();

                List<SelectListItem> listRol = profileUser.getRolesAvaliable(user.IdRol);

                ViewData["roles"] = listRol;

                return View(user);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Users-Editar")]
        public ActionResult Edit(int id, UserGestCor model, int roles)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.IdRol = roles;
                if (model.UpdateUser(id))
                {
                    TempData["AlertMessage"] = "USUARIO EDITADO CON EXITO";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al editar el usuario";
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
