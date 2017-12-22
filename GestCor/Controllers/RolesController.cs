using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            GestCorRol Rol = new GestCorRol();
            return View(Rol.GetRoles());
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(GestCorRol model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.SaveGestCorRoles())
                    return RedirectToAction("Index");
                else
                    return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            GestCorRol rol = new GestCorRol();
            return View(rol.GetRolesById(id));
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GestCorRol model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.UpdateGestCorRoles(id))
                    return RedirectToAction("Index");
                else
                    return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
