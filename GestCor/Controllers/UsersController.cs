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
        public ActionResult Index()
        {
            UserGestCor user = new UserGestCor();

            return View(user.GetUsers());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            try
            {
                Rol rol = new Rol();

                var rolList = rol.getRolesAvaliable();

                var listRol = new SelectList(rolList, "Id", "NameRol");

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
                    return RedirectToAction("Index");
                else
                    return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                UserGestCor profile = new UserGestCor();
                
                Rol rol = new Rol();
                
                var rolList = rol.getRolesAvaliable();
                
                var listRol = new SelectList(rolList, "Id", "NameRol");

                ViewData["roles"] = listRol;

                return View(profile.GetUsersById(id));
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
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
                    return RedirectToAction("Index");
                else
                    return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
